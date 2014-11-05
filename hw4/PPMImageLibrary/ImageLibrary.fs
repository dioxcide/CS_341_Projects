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

//Converts each individual head into a string from the list
//Reference Function found on stackoverflow! http://stackoverflow.com/questions/19469252/convert-integer-list-to-a-string
let rec ToStrings image =
   match image with
   | [i] -> i.ToString()
   | temp :: temp2 -> temp.ToString() + "  " + ToStrings temp2
   | [] -> ""

//Appends the list of newly found strings to the finalList
let rec private appendColors (image:int list list) (finalList)= 
  if image = [] then
    finalList
  else 
    let temp = ToStrings image.Head 
    let temp2 = [temp] @ finalList
    appendColors image.Tail temp2



// WriteP3Image:
//
// Writes the given image out to a text file, in "P3" format.  Returns true if successful,
// false if not.
//
let WriteP3Image(filepath:string, width:int, height:int, depth:int, image:int list list) = 

  //File.AppendAllText(filepath, "P3" + System.Environment.NewLine)
  //File.

  let dimensions = (string width) + " " + (string height) + System.Environment.NewLine
  let temp = (appendColors image [])
  System.IO.File.WriteAllLines(filepath, (["P3";dimensions;(string depth)+ System.Environment.NewLine;]@temp))

  true  // success

////////////////////////////////
//Goes through the single list add modifies the rgb values
let rec private _transformG (image: int list) (_grayscale: int list) = 
  if image = [] then
    _grayscale
  else
    let sum = (image.Head + image.Tail.Head + image.Tail.Tail.Head)/3
    let tempList = _grayscale @ [sum;sum;sum]
    _transformG image.Tail.Tail.Tail tempList

//Goes through the first list in order to create a grayscale of the image
let rec private transformG (image: int list list) (grayscale: int list list) = 
  if image = [] then 
    grayscale
  else
    let tempList = grayscale @ [(_transformG (image.Head) [])]
    transformG image.Tail tempList
//General image
let TransformGrayscale(width:int, height:int, depth:int, image:int list list) = 
  (transformG image [])

////////////////////////////////
//Modifies the value
let private invert (depth:int) (image:int)  =
    let currValue = depth - image
    currValue
//Applies the invert function to the image
let private _invertImg (depth:int) (image: int list)= 
    (List.map (invert depth) image)
//Transforms Traverses the first list
let TransformInvert(width:int, height:int, depth:int, image:int list list) = 
    (List.map (_invertImg depth) image)
   
/////////////////////////////////////////
//Traverses the list and reverses the rows
let rec private _FlipHorizontal (_flip: int list)  (image: int list) = 
   if image = [] then
    _flip
   else
    let tempList = [image.Head;image.Tail.Head;image.Tail.Tail.Head] @ _flip
    _FlipHorizontal tempList image.Tail.Tail.Tail 
//Applies the list map function with fliphorizontal 
let TransformFlipHorizontal(width:int, height:int, depth:int, image:int list list) = 
    (List.map (_FlipHorizontal []) image)

/////////////////////////////////
//Reverses the rows of the image
let TransformFlipVertical(width:int, height:int, depth:int, image:int list list) = 
  List.rev image

//////////////////////////////////
//Half working functions
let rec getColumnToRow(image:int list list) (row:int) (counter1:int) (counter2:int) (ColumnToRow:int list) = 
  if row = counter1 then
    ColumnToRow
  else
    let temp = ColumnToRow @ [image.Head.Item(counter2+2);image.Head.Item(counter2+1);image.Head.Item(counter2)]
    getColumnToRow image.Tail row (counter1+1) (counter2)  temp 

//Goes through the list and creates a row
let rec _Rotate90 (image:int list list) (row:int) (col:int) (rowCounter:int) (colCounter: int) (ColumnToRow: int list list) = 
  if row = colCounter then
    ColumnToRow
  else
    let temp = ([getColumnToRow image col 0 rowCounter []]) @ ColumnToRow
    _Rotate90 image row col (colCounter+1) (rowCounter+3) temp
    
//Initial Function to call everything
let RotateRight90(width:int, height:int, depth:int, image:int list list) = 
  let rotated = List.rev image
  (_Rotate90 image width height 0 0 [])