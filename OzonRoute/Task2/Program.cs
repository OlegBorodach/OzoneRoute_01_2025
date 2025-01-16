using var input = new StreamReader(Console.OpenStandardInput());
using var output = new StreamWriter(Console.OpenStandardOutput());

var n = Convert.ToInt16(input.ReadLine());

for (short j = 0; j < n; j++)
{
    var len = int.Parse(input.ReadLine());
    var dataIn = input.ReadLine().AsSpan();
    var dataOut=input.ReadLine().AsSpan();

    if (dataIn.Length!=dataOut.Length)
    {
        Console.WriteLine("no");
        continue;
    }
    var arr = new List<int>(len);

    var start = 0;
    for (var i = 0; i < dataIn.Length; i++)
    {
        if (dataIn[i] == ' ')
        {
            var c = dataIn.Slice(start, i - start);
            start = i + 1;
            arr.Add(Convert.ToInt32(c.ToString()));
        }
    }
    arr.Add(Convert.ToInt32(dataIn[start ..].ToString()));
    var resArr=arr.Order().ToList();

    start = 0;
    var index = 0;
    var result = true;
    for (var k = 0; k < dataOut.Length; k++)
    {
        if (dataOut[k] == ' ')
        {
            var c = dataOut.Slice(start, k - start);
            var res=int.TryParse(c, out var val);
            if (!res)
            {
                result = false;
                break;
            }
            if (resArr[index]!=val)
            {
                result = false;
                break;
            }
            start = k + 1;
            index++;
        }

        if (k==dataOut.Length-1)
        {
            var res=int.TryParse(dataOut[start ..], out var val);
            if (!res)
            {
                result = false;
                break;
            }
            if (resArr[index]!=val)
            {
                result = false;
                break;
            }
        }
    }
    Console.WriteLine(result?"yes":"no");
}