namespace day1

open System.IO

module Part1 =
    let directions =
        File.ReadLines("../../../day1.txt")
        |> Seq.map (fun s -> s.Trim())
        |> Seq.map (fun s -> s.[0..0], int s.[1..])
        |> Seq.toList

    let updateTotal acc total =
        if acc = 0 then total + 1 else total

    let updateAcc acc value direction =
        let result = if direction = "R" then acc + value else acc - value
        if result < 0 then result + 100 elif result >= 100 then result - 100 else result

    let finalAcc, finalTotal =
        directions
        |> List.fold (fun (acc, total) (dir, value) ->
            let newAcc = updateAcc acc (value%100) dir
            let newTotal = updateTotal newAcc total
            (newAcc, newTotal)
        ) (50, 0)

