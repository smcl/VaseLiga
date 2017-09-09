namespace VaseLiga

open System
open System.IO
open System.Net
open System.Text
open System.Xml


type VaseligaResult (date:string, time:string, league:string, opponent:string, location:string, result:string, comment: string) =
    member this.DateTime = if String.IsNullOrEmpty date then System.DateTime() else System.DateTime.ParseExact(String.Format("{0} {1}", date.Trim(), time.Trim()), "dd.MM.yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture)
    member this.Month = DateTime(this.DateTime.Year, this.DateTime.Month, 1)
    member this.Opponent = opponent.Trim()
    member this.Location = location.Trim()
    member this.Result = result.Trim()
    member this.Command = comment.Trim()
    override this.ToString() = 
        String.Format("{0} - {1}", this.Result, this.Opponent)


type CsvRow (row:string) =
    member this.Columns = row.Split [|';'|]

    member this.GetColumn n = this.Columns.[n]


type VaseligaReader () =
    let urlFormat = "http://www.vaseliga.cz/cz/hraci/{0}/?tab=leagues_matches&citysport_id={1}&csv=1&sort=datum&order=1#tabs-2"

    let splitLines (s:string) = 
        List.ofSeq(s.Split([|'\n'|]))   

    let createResult (row:CsvRow) =
        VaseligaResult((row.GetColumn 0), (row.GetColumn 1), (row.GetColumn 2), (row.GetColumn 3), (row.GetColumn 4), (row.GetColumn 5), (row.GetColumn 6))   

    member this.Get(user:string, citySport:int) =
        let wc = new WebClient()
        let iso8859_2 = CodePagesEncodingProvider.Instance.GetEncoding("iso-8859-2")

        wc.DownloadData(String.Format(urlFormat, user, citySport))
        |> iso8859_2.GetString
        |> splitLines
        |> Seq.skip 1
        |> Seq.map CsvRow
        |> Seq.filter (fun r -> r.Columns.Length = 9)
        |> Seq.map (createResult)