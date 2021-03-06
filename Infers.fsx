#I "Libs/Infers.Core/bin/Debug"
#I "Libs/Infers/bin/Debug"
#I "Libs/Infers.Rep/bin/Debug"
#I "Libs/Infers.Toys/bin/Debug"

#r "Infers.dll"
#r "Infers.Rep.dll"
#r "PPrint.dll"
#r "Infers.Toys.dll"

open PPrint
open Infers.Toys
open Infers.Rep
open Infers

let inline time (x: Lazy<'x>) =
  let timer = System.Diagnostics.Stopwatch.StartNew ()
  let r = x.Force ()
  printfn "Took %A" timer.Elapsed
  r
