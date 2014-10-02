// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
#light

let rec equal (L1:'a list)(L2: 'a list) = 
    if L1 = [] && L2 = [] then
     printfn "True"
     ()

    else if (List.length L1) = (List.length L2) then
     if L1.Head = L2.Head then
      equal L1.Tail L2.Tail
     else 
      printfn "False"
      ()
    
    else if (List.length L1) <> (List.length L2) then 
      printfn "False"
      ()

let rec sumMiddleTwoElements (L: int list) index1 index2 currIndex1 currIndex2 value1 value2 = 
    if index1 = currIndex1 && index2 = currIndex2 then
     let value = value1 + value2
     let avg = value/2
     avg
    else if index1 = currIndex1 then
        sumMiddleTwoElements L.Tail index1 index2 currIndex1 currIndex2 List.head 0


let median (list: int list) = 
    let length = list.Length
    let halfL = length/2

    if length%2 = 0 then
     sumMiddleTwoElements list halfL-1 halfL 0 0 0 0
    else
     list.Item(halfL)

[<EntryPoint>]
let main argv = 
    let list1 = [42;78;12]
    let list2 = [42;85;12]
    equal list1 list2
    0 // return an integer exit code
