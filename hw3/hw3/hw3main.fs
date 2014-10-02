// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
#light
//
// ReadFile: reads a file line by line, returning a list
// of strings, one per line.
//
let ReadFile filename =
 [ for line in System.IO.File.ReadLines(filename) -> line ]
//
// ParseLine: given a line from the rainfall file, parses this
// string into a tuple (year, values) where values is a list of
// the 12 rainfall data values for year.
//
let ParseLine (line:string) =
 let strings = line.Split('\t')
 let strlist = Array.toList(strings)
 let year = System.Int32.Parse(strlist.Head)
 let values = List.map System.Double.Parse strlist.Tail
 (year, values)
//
// Given a tuple (year, values), prints these to the console
//
let PrintYearData (year:int, values:double list) =
 printfn ""
 printfn "%A: %A" year values


let ComputeAvg (years:int, values: double list) = 
    let temp = snd(years, values)
    let year = fst(years, values)
    let avg = List.average temp
    printf "%d: " year
    printfn "%f\n" (avg)
    0

let rec ComputerMonthAvg (values:double list) index indexTracker = 
    if index = indexTracker then
     values.Head
    else
     let currIndex = indexTracker + 1
     ComputerMonthAvg values.Tail index currIndex

let rec parseLinebyLine File Jan Feb Mar Apr May Jun July Aug Sep Oct Nov Dec Size = 
    if File = [] then
     printfn "January: %A" (Jan/(float Size))
     printfn "February: %A" (Feb/(float Size))
     printfn "March: %A" (Mar/(float Size))
     printfn "April: %A" (Apr/(float Size))
     printfn "May: %A" (May/(float Size))
     printfn "June: %A" (Jun/(float Size))
     printfn "July: %A" (July/(float Size))
     printfn "August: %A" (Aug/(float Size))
     printfn "September: %A" (Sep/(float Size))
     printfn "October: %A" (Oct/(float Size))
     printfn "November: %A" (Nov/(float Size))
     printfn "December: %A" (Dec/(float Size))
     []
    else 
     let (year, values) = ParseLine File.Head
     
     let rainfall = snd(year,values)

     let one  = Jan + ComputerMonthAvg rainfall 0 0
     let two  = Feb + ComputerMonthAvg rainfall 1 0
     let three  = Mar + ComputerMonthAvg rainfall 2 0
     let four  = Apr + ComputerMonthAvg rainfall 3 0
     let five  = May + ComputerMonthAvg rainfall 4 0
     let six  = Jun + ComputerMonthAvg rainfall 5 0
     let seven  = July + ComputerMonthAvg rainfall 6 0
     let eigth  = Aug + ComputerMonthAvg rainfall 7 0
     let nine  = Sep + ComputerMonthAvg rainfall 8 0
     let ten  = Oct + ComputerMonthAvg rainfall 9 0
     let eleven  = Nov + ComputerMonthAvg rainfall 10 0
     let twelve  = Dec + ComputerMonthAvg rainfall 11 0
     
     ComputeAvg (year, values)
     
     parseLinebyLine File.Tail one two three four five six seven eigth nine ten eleven twelve Size

//
// Main:
//
[<EntryPoint>]
let main argv =
 // read entire file as list of strings:
 let file = ReadFile "rainfall-midway.txt"
 printfn "Average Rain Fall Per Year\n"
 parseLinebyLine file 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 (List.length file)

 //PrintYearData (year, values)
 // done:
 printfn ""
 printfn ""
 System.Console.ReadKey(true) |> ignore
 0 // return 0 => success
