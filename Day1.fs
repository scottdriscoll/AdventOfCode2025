namespace day1

open System.IO

module Load =
    let directions =
        File.ReadLines("../../../day1.txt")
        |> Seq.map (fun s -> s.Trim())
        |> Seq.map (fun s -> s.[0..0], int s.[1..])

module Part1 =
    let updateTotal acc total =
        if acc = 0 then total + 1 else total

    let updateAcc acc value direction =
        let result = if direction = "R" then acc + value else acc - value
        if result < 0 then result + 100 elif result >= 100 then result - 100 else result

    let finalAcc, finalTotal =
        Load.directions
        |> Seq.fold (fun (acc, total) (dir, value) ->
            let newAcc = updateAcc acc (value%100) dir
            let newTotal = updateTotal newAcc total
            (newAcc, newTotal)
        ) (50, 0)

module Part2 =
    let updateAcc acc value direction total =
        let rotations = int value / 100
        let modValue = value % 100
        let result = if direction = "R" then acc + modValue else acc - modValue
        match result with
        | r when r < 0 ->
            (r + 100, if acc = 0 then total + rotations else total + rotations + 1)
        | r when r >= 100 ->
            (r - 100, if acc = 0 then total + rotations else total + rotations + 1)
        | 0 ->
            (0, total + rotations + 1)
        | r ->
            (r, total + rotations)

    let finalAcc, finalTotal =
        Load.directions
        |> Seq.fold (fun (acc, total) (dir, value) ->
            let newAcc, newTotal = updateAcc acc value dir total
            (newAcc, newTotal)
        ) (50, 0)
