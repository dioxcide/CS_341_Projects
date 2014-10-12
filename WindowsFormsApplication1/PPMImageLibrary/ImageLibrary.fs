module PPMImageLibrary

#light

//
// F#-based PPM image library.
//
// Antonio Villarreal
// U. of Illinois, Chicago
// CS341, Fall 2014
// Homework 4
//

//
// DebugOutput:
//
// Outputs to console, which appears in the "Output" window pane of
// Visual Studio when you run with debugging (F5).
//
let rec private OutputImage(image:int list list) = 
  match image with
  | [ ] -> printfn "**END**"
  |  _  -> printfn "%A" image.Head
           OutputImage(image.Tail)
           
let DebugOutput(width:int, height:int, depth:int, image:int list list) =
  printfn "**HEADER**"
  printfn "W=%A, H=%A, D=%A" width height depth
  printfn "**IMAGE**"
  OutputImage(image)


//
// TransformFirstRowWhite:
//
// An example transformation: replaces the first row of the given image
// with a row of all white pixels.
//
let rec BuildRowOfWhite cols white = 
  match cols with
  | 0 -> []
  | _ -> // 1 pixel, i.e. RGB value, followed by remaining pixels:
         white :: white :: white :: BuildRowOfWhite (cols-1) white

let TransformFirstRowWhite(width:int, height:int, depth:int, image:int list list) = 
  // first row all white :: followed by rest of original image
  BuildRowOfWhite width depth :: image.Tail


//
// WriteP3Image:
//
// Writes the given image out to a text file, in "P3" format.  Returns true if successful,
// false if not.
//
let WriteP3Image(filepath:string, width:int, height:int, depth:int, image:int list list) = 
  //
  //
  // TODO!
  //
  //
  true  // success