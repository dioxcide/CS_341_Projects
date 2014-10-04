#light

let UnitTest test = 
  if test then
    "correct"
  else
    "incorrect"


// 
// contains e L
//
let rec contains e L = 
  if L = [] then
    false
  else if L.Head = e then
    true
  else 
    contains e L.Tail


// 
// subset L1 L2
//
let rec subset L1 L2 = 
  if L1 = [] then
    true
  else if (contains L1.Head L2) then
    subset L1.Tail L2
  else 
    false


//
// multiply L1 L2
//
let rec multiply L1 L2 = 
    if L1 = [] && L2 = [] then
      ([] : int list)
    else 
       [(L1.Head * L2.Head)] @ multiply L1.Tail L2.Tail
//
// where e L
//

let rec find e L (index: int) = 
    if L = [] then
     0
    else if L.Head = e then
     index
    else 
     let temp = index + 1
     find e L.Tail temp


let where e L = 
    find e L 1


//
// max L
//
let rec compMax (L: int list) currMax = 
    if L = [] then
        currMax
    else if L.Head >= currMax then
        compMax L.Tail L.Head
    else 
        compMax L.Tail currMax

let max (L: int list) =
    let  temp = compMax L L.Head
    temp



[<EntryPoint>]
let main argv = 
  printfn "contains: %A" (UnitTest ((contains 10 [2;88;6;44;10]) = true))
  printfn "contains: %A" (UnitTest ((contains 11 [2;88;6;44;10]) = false))
  printfn "contains: %A" (UnitTest ((contains 10 []) = false))

  printfn ""

  printfn "subset: %A" (UnitTest ((subset [44;2;88] [2;88;6;44;10]) = true))
  printfn "subset: %A" (UnitTest ((subset [44;2;88;11] [2;88;6;44;10]) = false))
  printfn "subset: %A" (UnitTest ((subset [] []) = true))

  printfn ""

  printfn "multiply: %A" (UnitTest ((multiply [1;2;3] [4;5;6] ) = [4;10;18]))
  printfn "multiply: %A" (UnitTest ((multiply [-1;22;0;10] [-1;0;22;12]) = [1;0;0;120]))
  printfn "multiply: %A" (UnitTest ((multiply [] []) = []))

  printfn ""

  printfn "where: %A" (UnitTest ((where 44 [2;88;6;44;10]) = 4))
  printfn "where: %A" (UnitTest ((where 11 [2;88;6;44;10]) = 0))
  printfn "where: %A" (UnitTest ((where 11 []) = 0))

  printfn ""

  printfn "max: %A" (UnitTest ((max [2;88;6;44;1;10]) = 88))
  printfn "max: %A" (UnitTest ((max [-2;-88;-6;-44;-10]) = -2))
  printfn "max: %A" (UnitTest ((max [11]) = 11))

  printfn ""
  printfn ""
  0 // return success