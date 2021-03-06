﻿// Copyright (C) by Vesa Karvonen

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System;

namespace Infers.Core {
  ///
  public abstract class Box {
    ///
    public volatile bool Ready;
    ///
    public abstract Object Get();
    ///
    public abstract void Set(Object o);
  }

  ///
  public class Box<T> : Box {
    ///
    public T Value;
    ///
    public override Object Get() {
      lock (this)
        while (!this.Ready)
          Monitor.Wait(this);
      return this.Value;
    }
    ///
    public override void Set(Object o) {
      Debug.Assert(!this.Ready);
      this.Value = (T)o;
      this.Ready = true;
    }
  }

  ///
  public static class StaticMap<Key, Value> {
    ///
    public static Box<Value> box;
  }

  ///
  public static class StaticMap {
    ///
    public static Box<Value> TryGet<Key, Value>() {
      return StaticMap<Key, Value>.box;
    }
    ///
    public static Box<Value> GetOrSet<Key, Value>(Box<Value> box) {
      Debug.Assert(null != box);
      return Interlocked.CompareExchange(ref StaticMap<Key, Value>.box, box, null);
    }
    ///
    public static Box<Value> Box<Value>() {
      return new Box<Value>();
    }
  }
}
