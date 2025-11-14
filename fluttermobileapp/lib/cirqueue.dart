class CircularQueue {
  final List<int?> _queue;
  int _front = -1;
  int _rear = -1;
  final int _size;

  CircularQueue(this._size) : _queue = List<int?>.filled(_size, null);

  // 큐가 가득 찼는지 확인
  bool isFull() {
    return (_rear + 1) % _size == _front;
  }

  // 큐가 비었는지 확인
  bool isEmpty() {
    return _front == -1;
  }

  // 단일 요소 추가 (enqueue)
  bool enqueue(int value) {
    if (isFull()) {
      print("큐가 가득 찼습니다.");
      return false;
    }
    if (_front == -1) _front = 0; // 첫 번째 요소 추가 시 front 설정
    _rear = (_rear + 1) % _size;
    _queue[_rear] = value;
    return true;
  }

  // 여러 개의 요소 추가 (enqueue)
  int enqueueList(List<int> values) {
    int addedCount = 0;
    for (var value in values) {
      if (!enqueue(value)) break; // 큐가 가득 차면 중단
      addedCount++;
    }
    return addedCount; // 추가된 요소 개수 반환
  }

  // 큐에서 데이터 제거 및 반환 (dequeue)
  int? dequeue() {
    if (isEmpty()) {
      print("큐가 비어 있습니다.");
      return null;
    }
    int? removedValue = _queue[_front];
    if (_front == _rear) {
      _front = _rear = -1; // 마지막 요소 제거 시 초기화
    } else {
      _front = (_front + 1) % _size;
    }
    return removedValue;
  }

  // 큐에서 여러 개의 데이터 제거 및 반환 (dequeueList)
  List<int> dequeueList(int count) {
    if (isEmpty()) {
      print("큐가 비어 있습니다.");
      return List<int>.empty();
    }

    List<int> removedValues = [];
    int dequeueCount = count > _size ? _size : count;

    for (int i = 0; i < dequeueCount; i++) {
      int? removedValue = dequeue();
      if (removedValue != null) {
        removedValues.add(removedValue);
      }
    }
    return removedValues;
  }

  // 현재 첫 번째 요소 확인 (peek)
  int? peek() {
    return isEmpty() ? null : _queue[_front];
  }

  // 큐의 모든 요소 출력 (디버깅용)
  void printQueue() {
    if (isEmpty()) {
      print("큐가 비어 있습니다.");
      return;
    }
    print("큐 상태: ${_queue.whereType<int>().toList()}");
  }
}

// void main() {
//   CircularQueue queue = CircularQueue(7);

//   queue.enqueue(10);
//   queue.enqueueList([20, 30, 40, 50]);
//   queue.printQueue(); // 출력: 큐 상태: [10, 20, 30, 40, 50]

//   print("추가된 개수: ${queue.enqueueList([60, 70, 80, 90])}"); // 출력: 추가된 개수: 2 (큐가 7칸이라 80, 90은 안 들어감)
//   queue.printQueue(); // 출력: 큐 상태: [10, 20, 30, 40, 50, 60, 70]

//   // 여러 개의 요소를 한 번에 제거
//   var dequeued = queue.dequeueList(3);
//   print("제거된 값: $dequeued"); // 출력: 제거된 값: [10, 20, 30]
//   queue.printQueue(); // 출력: 큐 상태: [40, 50, 60, 70]

//   // 큐에서 2개의 데이터 더 빼기
//   dequeued = queue.dequeueList(2);
//   print("제거된 값: $dequeued"); // 출력: 제거된 값: [40, 50]
//   queue.printQueue(); // 출력: 큐 상태: [60, 70]
// }
