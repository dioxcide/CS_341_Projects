module PPMImageLibrary
open System.IO
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

let rec private _appendColors (image:int list) (fileName:string) = 
  if image = [] then
    0
  else
    File.AppendAllText(fileName, (string image.Head) + " ")
    File.AppendAllText(fileName, (string image.Tail.Head) + " ")
    File.AppendAllText(fileName, (string image.Tail.Tail.Head)  + "      ")
    _appendColors (image.Tail.Tail.Tail) fileName
 
let rec private appendColors (image:int list list) (fileName:string) = 
  if image = [] then
    0
  else 
    _appendColors image.Head fileName
    appendColors image.Tail fileName
//
// WriteP3Image:
//
// Writes the given image out to a text file, in "P3" format.  Returns true if successful,
// false if not.
//
let WriteP3Image(filepath:string, width:int, height:int, depth:int, image:int list list) = 
  let fileName = filepath + ".txt"

  File.AppendAllText(fileName, "P3    ")
  //File.

  let dimensions = (string width) + " " + (string height)
  File.AppendAllText(fileName, dimensions)
  File.AppendAllText(fileName, (string depth))
  appendColors image fileName
  true  // success

let rec private _transformG (image: int list) (_grayscale: int list) = 
  if image = [] then
    _grayscale
  else
    let sum = (image.Head + image.Tail.Head + image.Tail.Head)/3
    let tempList = _grayscale @ [sum;sum;sum]
    _transformG image.Tail.Tail.Tail tempList

let rec private transformG (image: int list list) (grayscale: int list list) = 
  if image = [] then 
    grayscale
  else
    let tempList = grayscale @ [(_transformG (image.Head) [])]
    transformG image.Tail tempList

let TransformGrayscale(width:int, height:int, depth:int, image:int list list) = 
  (transformG image [])

let rec private _invertImg (image: int list) (_inverted: int list) (depth:int)= 
  if image = [] then
    _inverted
  else
    let currValue = depth - image.Head
    let tempList = _inverted @ [currValue]
    _invertImg image.Tail tempList depth

let rec private invertImg (image: int list list) (inverted: int list list) (depth:int) = 
  if image = [] then 
    inverted
  else
    let tempList = inverted @ [(_invertImg (image.Head) [] depth)]
    invertImg image.Tail tempList depth

let TransformInvert(width:int, height:int, depth:int, image:int list list) = 
  (invertImg image [] depth)
