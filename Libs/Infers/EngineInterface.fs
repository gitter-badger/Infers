﻿// Copyright (C) by Vesa Karvonen

namespace Infers

open System

type [<AbstractClass>] Rules () =
  inherit Attribute ()

type [<AbstractClass>] Rec<'x> () =
  abstract Get: unit -> 'x
  abstract Set: 'x -> unit
  // XXX This generates warning FS0050 as the IRecObj interface does not appear
  // in the signature.  This is an internal implementation detail that users
  // should never need to care about and makes it possible to invoke the typed,
  // user defined Get and Set functions dynamically without having to use
  // reflection.  If there is a better way to achieve this in F#, then I'd love
  // to know about it, because being able to have this kind of internal
  // functionality is a fairly common need.
  interface IRecObj with
   override this.GetObj () = box (this.Get ())
   override this.SetObj x = this.Set (unbox<'x> x)