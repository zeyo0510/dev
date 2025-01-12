library range;
import "dart:collection";
class Range extends Object with IterableMixin<int> {
  Range(int this.start, [int this.stop, int this.step = 1]) {
    if (stop == null ) { // reverse stop and start making start 0
      stop = start;
      start = 0;
    }
    if (step == 0) {
      throw new ArgumentError("step must not be 0");
    }
  }

  Iterator<int> get iterator {
      return new RangeIterator(start, stop, step);
  }

  int get length {
    if ((step > 0 && start > stop) || (step < 0 && start < stop)) {
      return 0;
    }
    return ((stop - start) / step).ceil();
  }

  bool get isEmpty => length == 0;

  String toString() {
    return step == 1 ?
      "Range($start, $stop)" : "Range($start, $stop, $step)";
  }

  bool every(bool f(int)) {
    for (int e in this) {
      if (f(e) == false) {
        return false;
      }
    }
    return true;
  }

  bool some(bool f(int)) {
    for (int e in this) {
      if (f(e)) {
        return true;
      }
    }
    return false;
  }

  void forEach(void f(int)) {
    for (int e in this) {
      f(e);
    }
  }

  List<int> filter(bool f(int)) {
    var l = new List<int>();
    for (int e in this) {
      if (f(e)) {
        l.add(e);
      }
    }
    return l;
  }

  List map(f(int)) {
    var l = new List();
    for (int e in this) {
      l.add(f(e));
    }
    return l;
  }

  bool operator ==(Range other) {
    return (other != null && start == other.start && stop == other.stop &&
            step == other.step);
  }

  int start;
  int stop;
  final int step;
}

class RangeIterator implements Iterator<int> {

  int _pos;
  final int _stop;
  final int _step;
  RangeIterator(int pos, int stop, int step)
    : _stop = stop,
      _pos = pos-step,
      _step = step;

  int get current {
    return _pos;
  }
  bool moveNext() {
    if (_step > 0  ? _pos +_step> _stop-1 : _pos+_step < _stop+1) {
      return false;
    }
    _pos += _step;
    return true;
  }
}
Range range(int start_inclusive, [int stop_exclusive, int step = 1]) => new Range(start_inclusive, stop_exclusive, step);
Range indices(lengthable) => new Range(0, lengthable.length);
