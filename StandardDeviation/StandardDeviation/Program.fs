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


[<EntryPoint>]
let main argv = 
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

    System.Console.ReadKey(true) |> ignore
    0 // return an integer exit code
