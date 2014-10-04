//
// F# program to analyze Chicago rainfall data.
//
// Antonio Villarreal
// U. of Illinois, Chicago
// CS341, Fall 2014
// Homework 3
//

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

 //Function computes the average for the given tuple
let ComputeAvg (years:int, values: double list) =  
    let avg = List.average values           //Computes the average
    printf "%d: " years                     //Prints out the average for the year
    printfn "%f" (avg)

//Function computes the sum of all the rainfall for a specific month
let rec SpecificMonthRainFall (values:double list) index indexTracker = 
    if index = indexTracker then //Returns the current value that we are currently at in the list
     values.Head
    else                        //Increment the tracker to keep track of list position
     let currIndex = indexTracker + 1
     SpecificMonthRainFall values.Tail index currIndex //Recursive call to traverse the list

// Prints all the data to the user
let printData Jan Feb Mar Apr May Jun July Aug Sep Oct Nov Dec (yMax:int, mMax:int, Max:double) (yMin:int, mMin:int, Min:double) Size =
    printfn "\n\tAverage Rainfall per Month\n" 
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
    printfn "\n\tMax Rainfall"
    printfn "%A, %A/%A" Max mMax yMax
    printfn "\n\tMin Rainfall"
    printfn "%A, %A/%A" Min yMin mMin

//Function traverses the List and returns the index the item is at
let rec traverseList (L:double list) item = 
    if L = [] then                  //Base Case
     0
    else if L.Head = item then     //If the item is found
     0
    else                            //Recursively call itself until the item is found
     1 + traverseList L.Tail item
     
//Finds the max number in the list and returns a tuple of the year month max value
let findMax (L:double list) year (y:int, m:int, max:double) = 
    let currMax = List.max L    //Finds max value

    if max <= currMax then      //Compares it to the current maxvalue to see if it should be replaced
        let month = traverseList L currMax + 1                  //Finds the month for the specific value
        let (years, months, newMax) = (year, month,currMax)     //Creates a tuple of the month year and value
        (years, months, newMax)                                 //returns the tuple

    else
        (y,m,max)              //If it shouldnt be replaced then it returns the original tuple

//Function finds the minimum amount of rainfall
let findMin (L:double list) year (y:int, m:int, min:double) =  
    let currMin = List.min L        //Gets minimum amount of rainfall from current list

    if min >= currMin then          //Compares it to the current min and creates a tuple if it needs to be updated
        let month = traverseList L currMin + 1
        let (years, months, newMin) = (year, month,currMin)
        (years, months, newMin)     //Returns the tuple

    else
        (y,m,min)               //If the current min is smaller than anything in the list it will return the original tuple

//Recursive function that reads in line the file list and calculates the averages max min rainfall
let rec parseLinebyLine File Jan Feb Mar Apr May Jun July Aug Sep Oct Nov Dec (yMax:int, mMax:int, Max:double) (yMin:int, mMin:int, Min:double) Size = 
    //Base case if the file is null then we print out the averages
    if File = [] then
     printData Jan Feb Mar Apr May Jun July Aug Sep Oct Nov Dec (yMax,mMax, Max) (yMin, mMin, Min) Size

     //[]
    else 
     let (year, values) = ParseLine File.Head       //Reads in the current line

     ComputeAvg (year, values)                      //Computes the average for each year

     let one  = Jan + SpecificMonthRainFall values 0 0  //Computes the sum for each month per year
     let two  = Feb + SpecificMonthRainFall values 1 0
     let three  = Mar + SpecificMonthRainFall values 2 0
     let four  = Apr + SpecificMonthRainFall values 3 0
     let five  = May + SpecificMonthRainFall values 4 0
     let six  = Jun + SpecificMonthRainFall values 5 0
     let seven  = July + SpecificMonthRainFall values 6 0
     let eigth  = Aug + SpecificMonthRainFall values 7 0
     let nine  = Sep + SpecificMonthRainFall values 8 0
     let ten  = Oct + SpecificMonthRainFall values 9 0
     let eleven  = Nov + SpecificMonthRainFall values 10 0
     let twelve  = Dec + SpecificMonthRainFall values 11 0

     let  (yMn, mMn, min) = (findMin values year (yMin, mMin, Min))     //Finds the max rainfall
     let  (yMx, mMx, max) = (findMax values year (yMax, mMax, Max))     //Finds the min rainfall
     
     //Recursive call the function to compute the averages 
     parseLinebyLine File.Tail one two three four five six seven eigth nine ten eleven twelve (yMx, mMx, max) (yMn, mMn, min) Size

//
// Main:
//
[<EntryPoint>]
let main argv =
 // read entire file as list of strings:
 let file = ReadFile "rainfall-midway.txt"

 printfn "\t**Rainfall Analysis Program **\n"
 
 let (year, values) = ParseLine file.Head 
 
 parseLinebyLine file 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 0.0 (0,0,0.0) (year,1,values.Head) (List.length file) 

 printfn "** Done **"
 System.Console.ReadKey(true) |> ignore
 0 // return 0 => success
