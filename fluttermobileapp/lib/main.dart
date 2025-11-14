import 'package:flutter/material.dart';

import 'dart:io';
import 'dart:typed_data';
//import 'package:protobuf/protobuf.dart';  // protobuf

import 'packet/dartany.pb.dart';
import 'cirqueue.dart';

//import 'dart:convert';  // dart:convert 임포트
//import 'dart:io';
//import 'package:http/http.dart';


void main() async {

  const int MAX_CIRQUEUE_SIZE = 10000;
  var reqLoginInfo = ReqLoginInfo();

  // 직렬화 (Protobuf)
  var data = reqLoginInfo.writeToBuffer();

  //data.length;
  // 데이터 앞에 길이 추가 (헤더)
  int packetId = 1;
  int packetLength = data.length + 8;
  
  // 패킷 앞에 길이를 추가하는 과정
  ByteData byteData = ByteData(packetLength); // 4바이트는 길이 정보
  byteData.setInt32(0, packetId, Endian.little); // 길이 정보를 앞에 추가 (4바이트)
  byteData.setInt32(4, packetLength, Endian.little); // 길이 정보를 앞에 추가 (4바이트)
  byteData.buffer.asUint8List().setAll(8, data); // 실제 데이터 뒤에 이어서 추가

  CircularQueue cirQueue = CircularQueue(MAX_CIRQUEUE_SIZE);
  //cirQueue.
  // 서버로 TCP 전송
  // var socket = await Socket.connect('127.0.0.1', 19004);
  
  // socket.listen(
  //   (List<int> data) {
  //     // String message = String.fromCharCodes(data);
  //     // print('서버로부터 받은 메시지: $message');
  //     cirQueue.enqueueList(data);
  //     while (true)
  //     {
  //       var pktHeaderBytes = cirQueue.dequeueList(8);
  //       if (pktHeaderBytes.isEmpty){ 
  //         break;
  //       }
  //     // nullcheck pktHeaderBytes
  //       Uint8List uint8Data = Uint8List.fromList(pktHeaderBytes);
  //       Uint8List firstPart = uint8Data.sublist(0, 4);
  //       Uint8List secondPart = uint8Data.sublist(4, 8);
  //       ByteData byteData = ByteData.sublistView(firstPart);
  //       int packID = byteData.getUint32(0, Endian.little);
  //       byteData = ByteData.sublistView(secondPart);
  //       int packSize = byteData.getUint32(0, Endian.little);


  //       int bodySize = packSize - 8;
  //       //print('Little-Endian 변환: $littleEndianValue'); // 출력: 2018915346 (0x78563412)
  //       if (bodySize < 0){
  //         // 에러
  //         break;
  //       }

  //       var pktBodyBytes = cirQueue.dequeueList(bodySize);
  //       if (pktBodyBytes.isEmpty){
  //         // 에러
  //         break;
  //       }
        
  //       Uint8List bodyPart = Uint8List.fromList(pktBodyBytes);
      
  //       //ByteData bodyBytes = ByteData.sublistView(bodyPart);
  //       //bodyBytes.getUint32(0, Endian.little);
  //       var message = ResLoginInfo.fromBuffer(bodyPart);
  //       print('수신한 메시지: $message');
  //     }      
  //   },
  //   onError: (error) {
  //     print('소켓 에러 발생: $error');
  //   },
  //   onDone: () {
  //     print('서버와의 연결이 종료됨');
  //     socket.destroy();
  //   },
  //   cancelOnError: true, // 에러 발생 시 자동으로 스트림 닫기
  // );

  // socket.add(byteData.buffer.asUint8List()); // 길이 + 데이터 전송
  // await socket.flush();
  // //await socket.close();

  // print("패킷 전송 완료.");

  // HttpClient client = HttpClient();
  // client.badCertificateCallback = (X509Certificate cert, String host, int port) => true;  // 인증서 우회 설정

  // // HttpClient로 요청 보내기
  // final request = await client.getUrl(Uri.parse('https://your-api-url.com'));
  // final response = await request.close();  // 요청 실행

  // // 응답 처리
  // if (response.statusCode == 200) {
  //   response.transform(utf8.decoder).listen((contents) {
  //     print('응답: $contents');
  //   });
  // } else {
  //   print('에러 발생: ${response.statusCode}');
  // }

  //ioClient.close(); // 요청이 끝난 후 클라이언트 닫기

  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        // This is the theme of your application.
        //
        // TRY THIS: Try running your application with "flutter run". You'll see
        // the application has a purple toolbar. Then, without quitting the app,
        // try changing the seedColor in the colorScheme below to Colors.green
        // and then invoke "hot reload" (save your changes or press the "hot
        // reload" button in a Flutter-supported IDE, or press "r" if you used
        // the command line to start the app).
        //
        // Notice that the counter didn't reset back to zero; the application
        // state is not lost during the reload. To reset the state, use hot
        // restart instead.
        //
        // This works for code too, not just values: Most code changes can be
        // tested with just a hot reload.
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.deepPurple),
      ),
      home: const MyHomePage(title: 'Flutter Demo Home Page'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key, required this.title});

  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".

  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  int _counter = 0;
  List<Widget> listViews = [];
  TextStyle textStyle = TextStyle(fontSize: 16, color: Colors.black);
  int _currentIndex = 0; // 현재 선택된 인덱스

  List<Widget> _pages = [];
  // final List<Widget> _pages = [
  //   Center(child: Text('홈 화면')),
  //   Center(child: Text('검색 화면')),
  //   Center(child: Text('설정 화면')),
  // ];

  //List<Widget> views = [];

  @override
  void initState() {
    super.initState();
    // _pages = [
    //   Center(child: Text('홈 화면')),
    //   Center(child: Text('검색 화면')),
    //   Center(child: Text('설정 화면')),
    // ];

    _pages = [
      Column(
        children: [
          Expanded(
            child: ListView(
              children: listViews
            ), 
          ),
          Container(
            padding: EdgeInsets.all(16),
            color: Colors.brown[200], // 배경색 추가
            child: Column(
              mainAxisSize: MainAxisSize.min, // 자식 크기만큼만 Column이 차지하도록 설정
              mainAxisAlignment: MainAxisAlignment.center,
              children: <Widget>[
                Text(
                  '$_counter',
                  // style은 build  
                  style: textStyle//Theme.of(context).textTheme.headlineMedium,
                ),
              ],
            ),
          ),
        //   Column(
        //   //color: Colors.brown[200],
        //   // Column is also a layout widget. It takes a list of children and
        //   // arranges them vertically. By default, it sizes itself to fit its
        //   // children horizontally, and tries to be as tall as its parent.
        //   //
        //   // Column has various properties to control how it sizes itself and
        //   // how it positions its children. Here we use mainAxisAlignment to
        //   // center the children vertically; the main axis here is the vertical
        //   // axis because Columns are vertical (the cross axis would be
        //   // horizontal).
        //   //
        //   // TRY THIS: Invoke "debug painting" (choose the "Toggle Debug Paint"
        //   // action in the IDE, or press "p" in the console), to see the
        //   // wireframe for each widget.
        //   mainAxisAlignment: MainAxisAlignment.center,
        //   children: <Widget>[
        //     //const Text('You have pushed the button this many times:'),
        //     Text(
        //       '$_counter',
        //       style: Theme.of(context).textTheme.headlineMedium,
        //     ),
        //   ],
        //  )
        ],
      ),
      Container(
        color : Colors.brown[200],
      ),
      Container(
        color: Colors.amber[200],
      )
    ];
  }

  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    
    textStyle = Theme.of(context).textTheme.headlineMedium ?? textStyle;
    //textStyle = Theme.of(context).textTheme.headlineMedium; // ✅ 여기서 Theme 사용
  }

  void _incrementCounter() {
    setState(() {
      // This call to setState tells the Flutter framework that something has
      // changed in this State, which causes it to rerun the build method below
      // so that the display can reflect the updated values. If we changed
      // _counter without calling setState(), then the build method would not be
      // called again, and so nothing would appear to happen.
      _counter++;
      listViews.add(PostContainer(title: 'post title $_counter'));
      //listView.add(Text('New Item ${widgets.length + 1}', style: TextStyle(fontSize: 20)));
      // Future.delayed(Duration.zero, () {
      //   setState(() {});
      // });
    });
  }

  @override
  Widget build(BuildContext context) {
    // This method is rerun every time setState is called, for instance as done
    // by the _incrementCounter method above.
    //
    // The Flutter framework has been optimized to make rerunning build methods
    // fast, so that you can just rebuild anything that needs updating rather
    // than having to individually change instances of widgets.
    return Scaffold(
      appBar: AppBar(
        // TRY THIS: Try changing the color here to a specific color (to
        // Colors.amber, perhaps?) and trigger a hot reload to see the AppBar
        // change color while the other colors stay the same.
        backgroundColor: Theme.of(context).colorScheme.inversePrimary,
        // Here we take the value from the MyHomePage object that was created by
        // the App.build method, and use it to set our appbar title.
        title: Text(widget.title),
      ),
      body: _pages[_currentIndex],
      // body: Column(
      //   children: [
      //     Expanded(
      //       child: ListView(
      //         children: listViews
      //       ), 
      //     ),
      //     Column(
      //     //color: Colors.brown[200],
      //     // Column is also a layout widget. It takes a list of children and
      //     // arranges them vertically. By default, it sizes itself to fit its
      //     // children horizontally, and tries to be as tall as its parent.
      //     //
      //     // Column has various properties to control how it sizes itself and
      //     // how it positions its children. Here we use mainAxisAlignment to
      //     // center the children vertically; the main axis here is the vertical
      //     // axis because Columns are vertical (the cross axis would be
      //     // horizontal).
      //     //
      //     // TRY THIS: Invoke "debug painting" (choose the "Toggle Debug Paint"
      //     // action in the IDE, or press "p" in the console), to see the
      //     // wireframe for each widget.
      //     mainAxisAlignment: MainAxisAlignment.center,
      //     children: <Widget>[
      //       //const Text('You have pushed the button this many times:'),
      //       Text(
      //         '$_counter',
      //         style: Theme.of(context).textTheme.headlineMedium,
      //       ),
      //     ],
      //    )
      //   ],
      // ),
      // body: ListView(
      //   children: [
      //     Container(
      //       padding: const EdgeInsets.all(10),
      //       child: const Text(
      //         "Feed Title 1",
      //         style: TextStyle(
      //           fontSize: 10,
      //           fontWeight: FontWeight.bold 
      //         ),
      //       ),
      //     ),
      //     Container(
      //       width: MediaQuery.of(context).size.width,
      //       height: 200,
      //       color: Colors.indigo,
      //     ),
      //     PostContainer(title: 'post title 1'), // funtion
      //   Column(
      //     //color: Colors.brown[200],
      //     // Column is also a layout widget. It takes a list of children and
      //     // arranges them vertically. By default, it sizes itself to fit its
      //     // children horizontally, and tries to be as tall as its parent.
      //     //
      //     // Column has various properties to control how it sizes itself and
      //     // how it positions its children. Here we use mainAxisAlignment to
      //     // center the children vertically; the main axis here is the vertical
      //     // axis because Columns are vertical (the cross axis would be
      //     // horizontal).
      //     //
      //     // TRY THIS: Invoke "debug painting" (choose the "Toggle Debug Paint"
      //     // action in the IDE, or press "p" in the console), to see the
      //     // wireframe for each widget.
      //     mainAxisAlignment: MainAxisAlignment.center,
      //     children: <Widget>[
      //       //const Text('You have pushed the button this many times:'),
      //       Text(
      //         '$_counter',
      //         style: Theme.of(context).textTheme.headlineMedium,
      //       ),
      //     ],
      //   )
      //   ],
      // ),
      bottomNavigationBar: BottomNavigationBar(
        currentIndex: _currentIndex, // 현재 선택된 탭
        onTap: (index) {
          setState(() {
            _currentIndex = index; // 탭 변경 시 상태 업데이트
          });
        },
        items: [
          BottomNavigationBarItem(icon: Icon(Icons.home), label: '홈'),
          BottomNavigationBarItem(icon: Icon(Icons.search), label: '검색'),
          BottomNavigationBarItem(icon: Icon(Icons.settings), label: '설정'),
        ],
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: _incrementCounter,
        tooltip: 'Increment',
        child: const Icon(Icons.add),
      ), // This trailing comma makes auto-formatting nicer for build methods.
    );
  }

  Widget PostContainer({String title='', Color color=Colors.indigo}){
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Container(
            padding: const EdgeInsets.all(10),
            child: Text(
              title,
              style: TextStyle(
                fontSize: 10,
                fontWeight: FontWeight.bold 
              ),
            ),
          ),
          Container(
            width: MediaQuery.of(context).size.width,
            height: 200,
            color: color,
          ),
      ],
    );
  }
}
