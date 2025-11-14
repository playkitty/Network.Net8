import socket 
import tensorflow as tf
import tensorflow_datasets as tfds
import re
import pandas as pd
import socket

import os
#현재 폴더 경로; 작업 폴더 기준
print(os.getcwd())

#현재 파일의 폴더 경로; 작업 파일 기준
print(os.path.dirname(os.path.realpath(__file__)))

#현재 파일 이름
print(__file__)

#현재 파일 실제 경로
print(os.path.realpath(__file__))

#현재 파일 절대 경로
print(os.path.abspath(__file__))

train_data = pd.read_csv('ChatBotData.csv')
train_data.head()

print('챗봇 샘플의 개수 :', len(train_data))

questions = []
for sentence in train_data['Q']:
    # 구두점에 대해서 띄어쓰기
    # ex) 12시 땡! -> 12시 땡 !
    sentence = re.sub(r"([?.!,])", r" \1 ", sentence)
    sentence = sentence.strip()
    questions.append(sentence)


answers = []
for sentence in train_data['A']:
    # 구두점에 대해서 띄어쓰기
    # ex) 12시 땡! -> 12시 땡 !
    sentence = re.sub(r"([?.!,])", r" \1 ", sentence)
    sentence = sentence.strip()
    answers.append(sentence)


# 서브워드텍스트인코더를 사용하여 질문, 답변 데이터로부터 단어 집합(Vocabulary) 생성
tokenizer = tfds.deprecated.text.SubwordTextEncoder.build_from_corpus(
    questions + answers, target_vocab_size=2**13)

# 시작 토큰과 종료 토큰에 대한 정수 부여.
START_TOKEN, END_TOKEN = [tokenizer.vocab_size], [tokenizer.vocab_size + 1]

# 시작 토큰과 종료 토큰을 고려하여 단어 집합의 크기를 + 2
VOCAB_SIZE = tokenizer.vocab_size + 2


def create_padding_mask(x):
  mask = tf.cast(tf.math.equal(x, 0), tf.float32)
  # (batch_size, 1, 1, key의 문장 길이)
  return mask[:, tf.newaxis, tf.newaxis, :]

def create_look_ahead_mask(x):
  seq_len = tf.shape(x)[1]
  look_ahead_mask = 1 - tf.linalg.band_part(tf.ones((seq_len, seq_len)), -1, 0)
  padding_mask = create_padding_mask(x) # 패딩 마스크도 포함
  return tf.maximum(look_ahead_mask, padding_mask)

def transformer(vocab_size, num_layers, dff,
                d_model, num_heads, dropout,
                name="transformer"):

  # 인코더의 입력
  inputs = tf.keras.Input(shape=(None,), name="inputs")

  # 디코더의 입력
  dec_inputs = tf.keras.Input(shape=(None,), name="dec_inputs")

  # 인코더의 패딩 마스크
  enc_padding_mask = tf.keras.layers.Lambda(
      create_padding_mask, output_shape=(1, 1, None),
      name='enc_padding_mask')(inputs)

  # 디코더의 룩어헤드 마스크(첫번째 서브층)
  look_ahead_mask = tf.keras.layers.Lambda(
      create_look_ahead_mask, output_shape=(1, None, None),
      name='look_ahead_mask')(dec_inputs)

  # 디코더의 패딩 마스크(두번째 서브층)
  dec_padding_mask = tf.keras.layers.Lambda(
      create_padding_mask, output_shape=(1, 1, None),
      name='dec_padding_mask')(inputs)

  # 인코더의 출력은 enc_outputs. 디코더로 전달된다.
  enc_outputs = encoder(vocab_size=vocab_size, num_layers=num_layers, dff=dff,
      d_model=d_model, num_heads=num_heads, dropout=dropout,
  )(inputs=[inputs, enc_padding_mask]) # 인코더의 입력은 입력 문장과 패딩 마스크

  # 디코더의 출력은 dec_outputs. 출력층으로 전달된다.
  dec_outputs = decoder(vocab_size=vocab_size, num_layers=num_layers, dff=dff,
      d_model=d_model, num_heads=num_heads, dropout=dropout,
  )(inputs=[dec_inputs, enc_outputs, look_ahead_mask, dec_padding_mask])

  # 다음 단어 예측을 위한 출력층
  outputs = tf.keras.layers.Dense(units=vocab_size, name="outputs")(dec_outputs)

  return tf.keras.Model(inputs=[inputs, dec_inputs], outputs=outputs, name=name)

def preprocess_sentence(sentence):
  # 단어와 구두점 사이에 공백 추가.
  # ex) 12시 땡! -> 12시 땡 !
  sentence = re.sub(r"([?.!,])", r" \1 ", sentence)
  sentence = sentence.strip()
  return sentence

# 최대 길이를 40으로 정의
MAX_LENGTH = 40

def evaluate(sentence):
  # 입력 문장에 대한 전처리
  sentence = preprocess_sentence(sentence)

  # 입력 문장에 시작 토큰과 종료 토큰을 추가
  sentence = tf.expand_dims(
      START_TOKEN + tokenizer.encode(sentence) + END_TOKEN, axis=0)

  output = tf.expand_dims(START_TOKEN, 0)

  # 디코더의 예측 시작
  for i in range(MAX_LENGTH):
    predictions = model(inputs=[sentence, output], training=False)

    # 현재 시점의 예측 단어를 받아온다.
    predictions = predictions[:, -1:, :]
    predicted_id = tf.cast(tf.argmax(predictions, axis=-1), tf.int32)

    # 만약 현재 시점의 예측 단어가 종료 토큰이라면 예측을 중단
    if tf.equal(predicted_id, END_TOKEN[0]):
      break

    # 현재 시점의 예측 단어를 output(출력)에 연결한다.
    # output은 for문의 다음 루프에서 디코더의 입력이 된다.
    output = tf.concat([output, predicted_id], axis=-1)

  # 단어 예측이 모두 끝났다면 output을 리턴.
  return tf.squeeze(output, axis=0)

def predict(sentence):
  prediction = evaluate(sentence)

  # prediction == 디코더가 리턴한 챗봇의 대답에 해당하는 정수 시퀀스
  # tokenizer.decode()를 통해 정수 시퀀스를 문자열로 디코딩.
  predicted_sentence = tokenizer.decode(
      [i for i in prediction if i < tokenizer.vocab_size])

  print('Input: {}'.format(sentence))
  print('Output: {}'.format(predicted_sentence))

  return predicted_sentence

model = tf.keras.models.load_model("ChatModel.model")

output = predict("영화 볼래?")
output = predict("고민이 있어")
output = predict("너무 화가나")
output = predict("카페갈래?")
output = predict("게임하고싶당")
output = predict("게임하자")

def run_server(host="127.0.0.1", port=12000):
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        s.bind((host, port))
        s.listen(1)
        conn, addr = s.accept()
        with conn:
            print('Connected by', addr)
        

def GetLine(svr):
	data = ""
	while(1):
		ch = svr.recv(1)
		data = data + ch
		if( ch == '\n' ):
			break
	return data;
	
def ParseHeader(data):
	res = data.split(' ')
	COMM = res[0]
	TRID = string.atoi(res[1])
	HPARAM = ""
	res_len = len(res)
	
	idx = 2
	while( idx < res_len ):
		if( idx > 2 ):
			HPARAM = HPARAM + ' '
		HPARAM = HPARAM + res[idx]
		idx = idx+1
		
	return COMM, TRID,HPARAM
	
def GetBodyLen(data):
	res = data.split(' ')
	res_len = len(res)
	return string.atoi(res[res_len-1])
	
def GetBody(svr, body_len):
	body = ""
	len = 0
	while(len < body_len):
		ch = svr.recv(1)
		body = body + ch
		len = len + 1
    
	return body

def server_connect(host, port):
	svr = socket(AF_INET, SOCK_STREAM)
	print('Connecting (', host, ':',port,')')
	ret = svr.connect_ex((host,port))
	if (ret == 0 ):
		print('Connected')
		return 1, svr
	else:
		print('ERROR :', ret)
		raw_input('Press any key to continue...')	
		return 0, svr
		
def server_close(svr):
	svr.close()
	print('Closed')
		
def send_msg(svr, cmd, trid, hparam):
	print('<<',cmd,trid,hparam)
	svr.send(cmd + ' ' + trid + ' ' + hparam + '\r\n')
	
def recv_msg(svr, cmd):
	global RECV_BODY
	
	data = GetLine(svr)
	if( len(data) == 0 ):
		print('ERROR : recv 0 byte')
		raw_input('Press any key to continue...')	
		return 0
		
	data = data[0:len(data)-2]
	COMM, TRID, HPARAM = ParseHeader(data)
	BODY = ""
	BODY_LEN = 0
	if( existBody(COMM) == 1 ):
		BODY_LEN =  GetBodyLen(HPARAM)
		if( BODY_LEN > 0 ):
			BODY = GetBody(svr, BODY_LEN)
			RECV_BODY = BODY
			
	print('>> %s %d %s' % (COMM,TRID,HPARAM))
	if( COMM == cmd ):
		return 1
	else:
		print('ERROR : recv err code', COMM)
		raw_input('Press any key to continue...')	
		return 0