// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

let rec _stddev L mean accum = 
    match L with
    | [] -> accum
    | _  -> let head = L.Head 
            let computation = head - mean
            let squared = computation * computation
            let accum2 = squared + accum
            _stddev L.Tail mean accum2

let stddev (L:float list) = 
    let sum = List.sum L
    let mean = sum/(float L.Length)
    let addition = _stddev L mean 0.0
    let fraction = (float addition) / (float L.Length)
    let dev = System.Math.Sqrt(fraction)
    printfn "%A" dev

let rec tailElement (L) = 
    match L with
    | []    -> 0
    | x::[] -> x
    | _     -> tailElement(L.Tail)
               
    
let absValue head: int = 
    if head < 0 then
        -head
    else 
        head

let rec absList (L) tempList = 
    match L with
    | []    -> tempList
    | _     -> if L.Head < 0 then
                    let temp = tempList @ [-L.Head] 
                    absList L.Tail temp
               else 
                    let temp = tempList @ [L.Head]
                    absList L.Tail temp

let rec absListT (L) = 
    match L with
    | []    -> []
    | _     -> let temp2 = [absValue (L.Head)]
               temp2 @ absListT L.Tail 

let absHigherOrder (L) =
    let temp = List.map absValue L
    temp

let _prependToEach prefix (head:string) = 
    let temp = prefix + head
    temp

let prependToEach prefix (L:string list) = 
    (List.map (_prependToEach prefix) L)

let rec mapby2 (F, L) = 
    match L with
    | [] -> []
    | _  -> F(L.Head,L.Tail.Head) :: mapby2 (F, L.Tail.Tail)


let rec merge (L,L2) = 
    match L,L2 with
    | [], [] -> []
    | [], _  -> L2
    | _, []  -> L
    | _, _   -> if L.Head < L2.Head then
                    L.Head :: merge (L.Tail, L2)
                else
                    L2.Head :: merge (L, L2.Tail)

let zero(x) =
  if x = 0 then 
    true 
  else 
    false

let highsatisfiesd f (L1:int list) = 
    let L2 = List.map(fun x -> if f(x) then 1 else 0) L1
    (List.sum L2)

let rec tailRecSatis f (L1:int list) satis =
    if L1 = [] then
        satis 
    else if f (L1.Head) then
        tailRecSatis f L1.Tail (1+satis)
    else 
        tailRecSatis f L1.Tail satis

let rec nonTailSatis f L1 =
    if L1 = [] then
        0
    else if f (L1.Head) then
        1 + nonTailSatis f L1.Tail
    else 
        0 + nonTailSatis f L1.Tail

let add x (y:int) = 
    let temp = x + y
    temp

let rec map2 F L1 L2 = 
    if L1 = [] && L2 = [] then
        []
    else
        (F L1.Head L2.Head ) :: map2 F L1.Tail L2.Tail 

let rec tailMap2 F L1 L2 newList = 
    if L1 = [] && L2 = [] then
        (List.rev newList)

    else
        tailMap2 F L1.Tail L2.Tail (F L1.Head L2.Head ::newList)

let _NumLambda (L2:int list )(x:int) =
    List.find (fun y -> if y = x then true else false) L2

let NumInCommon (L1:int list) (L2: int list) = 
    let temp = List.map (_NumLambda L2) L1
    (List.sum temp)

let rec _TailNumInCommon L1 L2 contains = 
    if L2 = [] then
        if contains > 0 then
            1
        else 
            0
    else if L2.Head = L1 then
        _TailNumInCommon L1 L2.Tail 1 + contains
    else 
        _TailNumInCommon L1 L2.Tail 0 + contains

let rec TailNumInCommon L1 L2 (contains:int) = 
    if L1 = [] then
        contains
    else 
        TailNumInCommon L1.Tail L2 ((_TailNumInCommon L1.Head L2 0) + contains) 

let rec _NotTailInCommon L1 L2 = 
    if L2 = [] then
        0
    else if L2.Head = L1 then
        1 + _NotTailInCommon L1 L2.Tail

    else 
        0 + _NotTailInCommon L1 L2.Tail

let rec NotTailInCommon L1 L2 = 
    if L1 = [] then
        0
    else if (_NotTailInCommon L1.Head L2) > 0 then
        1 + NotTailInCommon L1.Tail L2
    else 
        0+ NotTailInCommon L1.Tail L2
        
[<EntryPoint>]
let main argv = 
    let num6 = NotTailInCommon [1;2;3] [10;3;99;4;1] 
    printfn "Practice: %A " num6

    let num5 = TailNumInCommon [1;2;3] [10;3;99;4;1] 0
    printfn "Practice: %A " num5

    //let num = NumInCommon [1;2;3] [10;3;99;4;1]
    //printfn "Practice2: %A" num

    let temp5 = map2 (add) [1;2;3] [4;5;6]
    printfn "Practice1: %A" temp5

    let temp6 = tailMap2 (add) [1;2;3] [4;5;6] []
    printfn "Practice1.2: %A" temp6

    let l3 = highsatisfiesd zero [12;324;0;52]
    printfn "Higher: %A: " l3

    let l3 = tailRecSatis zero [12;324;0;52] 0
    printfn "Higher: %A: " l3

    let l5 = nonTailSatis zero [12;324;0;52] 
    printfn "Higher: %A: " l5


    let temp = merge ([1;100;200], [2;3;4;100;101;300])

    printfn "Merge: %A" temp

    let temp = mapby2 ((fun (x,y) -> x + y), [1;2;3;4;5;6])
    printfn "MapBy2: %A" temp

    let l = [2.0;4.0;4.0;4.0;5.0;5.0;7.0;9.0]
    stddev l

    let l2 = [9;8;7]
    let temp = tailElement l2
    printfn "%A" temp

    let l3 = [0;-7;4]
    let temp = absList l3 []
    printfn "%A" temp

    let temp = absListT l3 
    printfn "%A" temp

    let temp = absHigherOrder l3 
    printfn "%A" temp

    let temp = List.reduce (fun x y -> x+y) l3 
    printfn "%A" temp

    let L = ["www.google.com";"www.yahoo.com";"www.bing.com"]

    let completeURLs = prependToEach "http://" L
    printfn "Complete URLS %A" completeURLs

    System.Console.ReadKey(true) |> ignore
    0 // return an integer exit code
