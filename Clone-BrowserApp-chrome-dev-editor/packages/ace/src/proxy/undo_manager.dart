part of ace.proxy;

class _UndoManagerProxy extends HasProxy implements UndoManager {
  
  bool get hasRedo => call('hasRedo');
  
  bool get hasUndo => call('hasUndo');
  
  _UndoManagerProxy._(js.JsObject proxy) : super(proxy);
  
  void onExecuted(List<UndoManagerDelta> deltas, bool merge) => 
      throw new UnsupportedError('This method may only be called internally.');
  
  Range redo({bool select: true}) => _range(call('redo', [!select]));
  
  void reset() => call('reset');
  
  Range undo({bool select: true}) => _range(call('undo', [!select]));

  void markClean() => call('markClean');

  bool get isClean => call('isClean');
}

/// A base class for implementing [UndoManager].
abstract class UndoManagerBase extends HasProxy implements UndoManager {
  
  EditSession _session;
  /// The [EditSession] whose undo stack this undo manager manages; may be 
  /// `null` until the first call to [UndoManager.onExecuted].
  EditSession get session => _session;
  
  UndoManagerBase() : super(_jsObject()) {
    _proxy['execute'] = (options) {
      if (_session == null) {
        _session = new _EditSessionProxy._(options['args'][1]);
      }   
      List<UndoManagerDelta> deltas =_list(options['args'][0]).map(
          _undoManagerDelta).toList(growable: false);      
      bool merge = options['merge'];
      onExecuted(deltas, merge);
    };
    _proxy['hasRedo'] = () => hasRedo;
    _proxy['hasUndo'] = () => hasUndo;    
    _proxy['redo'] = ([bool dontSelect = false]) => redo(select: !dontSelect);
    _proxy['reset'] = () => reset();
    _proxy['undo'] = ([bool dontSelect = false]) => undo(select: !dontSelect); 
    _proxy['markClean'] = markClean;
    _proxy['isClean'] = () => isClean;
  }
  
  Future _onDispose() {
    _proxy.deleteProperty('execute');
    _proxy.deleteProperty('hasRedo');
    _proxy.deleteProperty('hasUndo');    
    _proxy.deleteProperty('redo');
    _proxy.deleteProperty('reset');
    _proxy.deleteProperty('undo');
    _proxy.deleteProperty('markClean');
    _proxy.deleteProperty('isClean');
    return new Future.value();
  }
}
