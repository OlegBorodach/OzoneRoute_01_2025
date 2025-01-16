
using var input = new StreamReader(Console.OpenStandardInput());
using var output = new StreamWriter(Console.OpenStandardOutput());

var k = Convert.ToInt16(input.ReadLine());

for (short j = 0; j < k; j++)
{
    // кол-во заказов
    var n = int.Parse(input.ReadLine());

    var arrivalStr = input.ReadLine().AsSpan();
    // моменты времени заказов
    var arrivals = ParseIntArray(arrivalStr, n);

    var orders = new Order[n];
    var result = new int[n];
    for (int i = 0; i < orders.Length; i++)
    {
        orders[i].Index = i;
        orders[i].Time = arrivals[i];
        result[i] = -1;
    }

    // кол-во грузовых машин
    var m = int.Parse(input.ReadLine());
    Machine[] machines = new Machine[m];

    for (int l = 0; l < m; l++)
    {
        var str = input.ReadLine().AsSpan();
        var dat = ParseIntArray(str, 3);
        machines[l].Start = dat[0];
            machines[l].End= dat[1];
                machines[l].Capacity= dat[2];
        machines[l].Index = l + 1;
    }

    Array.Sort(machines, (x, y) =>
    {
        var res = x.Start.CompareTo(y.Start);
        return res == 0 ? x.Index.CompareTo(y.Index) : res;
    });
    Array.Sort(orders, (x, y) => x.Time.CompareTo(y.Time));

    var lastOrder = 0;
    foreach (var machine in machines)
    {
        var capacity = machine.Capacity;
        for (int d = lastOrder; d < orders.Length; d++)
        {
            if(capacity==0) break;
            if (orders[d].Time>machine.End) break;

            //если попало по времени и машина не полная
            if (orders[d].Time >= machine.Start && orders[d].Time <= machine.End )
            {
                result[orders[d].Index] = machine.Index;
                capacity--;
                lastOrder = d+1;
            }
        }
    }
    var sb = result.Aggregate(new System.Text.StringBuilder(), (s, i) => s.Append($"{i} "));
    output.WriteLine(sb.ToString());
}

int[] ParseIntArray(ReadOnlySpan<char> str, int sizeArray)
{
    var intArray = new int[sizeArray];
    var t = 0;
    var start = 0;
    for (var i = 0; i < str.Length; i++)
    {
        if (str[i] == ' ')
        {
            var c = str.Slice(start, i - start);
            start = i + 1;
            intArray[t] = Convert.ToInt32(c.ToString());
            t++;
        }
    }

    intArray[t] = Convert.ToInt32(str[start ..].ToString());
    return intArray;
}

struct Machine
{
    public int Start { get; set; }
    public int End { get; set; }
    public int Capacity { get; set; }
    public int Index { get; set; }
}

struct Order
{
    public int Index { get; set; }
    public int Time { get; set; }
    public bool Processed { get; set; }
}