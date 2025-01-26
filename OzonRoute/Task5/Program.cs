using var input = new StreamReader(File.Open(Path.Combine(Directory.GetCurrentDirectory(), "6"), FileMode.Open));

var k = Convert.ToInt16(input.ReadLine());

for (short j = 0; j < k; j++)
{
    // кол-во заказов
    var str = input.ReadLine().Split();
    var n = byte.Parse(str[0]);

    int i;
    var list = new char[n][];
    var indexA = -1;
    var indexB = -1;
    var aNum = -1;
    var bNum = -1;

    for (i = 0; i < n; i++)
    {
        list[i] = input.ReadLine().ToCharArray();
        var mc = new Span<char>(list[i]);

        if (indexA == -1)
        {
            if (mc.Contains('A'))
            {
                indexA = mc.IndexOf('A');
                aNum = i;
            }
        }

        if (indexB == -1)
        {
            if (mc.Contains('B'))
            {
                indexB = mc.IndexOf('B');
                bNum = i;
            }
        }
    }

    if (aNum < bNum)
    {
        UpList(list, indexA, 'a', aNum);
        DownList(list, indexB, 'b', bNum);
    }

    if (aNum > bNum)
    {
        UpList(list, indexB, 'b', bNum);
        DownList(list, indexA, 'a', aNum);
    }

    if (aNum == bNum)
    {
        if (indexA < indexB)
        {
            UpList(list, indexA, 'a', aNum);
            DownList(list, indexB, 'b', bNum);
        }
        else
        {
            UpList(list, indexB, 'b', bNum);
            DownList(list, indexA, 'a', aNum);
        }
    }

    foreach (var c in list)
    {
        Console.WriteLine(string.Join("", c));
    }
}


void UpList(IReadOnlyList<char[]> list, int index, char symbol, int nomerStroki)
{
    // если чётная строка
    var n = nomerStroki + 1;
    if (n % 2 == 0)
    {
        Up(list, index, nomerStroki, symbol);
        Left(list[0], index, symbol);
    }
    else
    {
        Left(list[nomerStroki], index, symbol);
        Up(list, 0, nomerStroki, symbol);
    }
}

void DownList(IReadOnlyList<char[]> list, int index, char symbol, int nomerStroki)
{
    var n = nomerStroki + 1;
// если чётная строка
    if (n % 2 == 0)
    {
        var from = Down(list, index, nomerStroki + 1, symbol);
        Right(list[^1], from, symbol);
    }
    else
    {
        if (list[nomerStroki].Length != index + 1)
        {
           Right(list[nomerStroki], index + 1, symbol);
        }
        if (nomerStroki != list.Count )
        {
            Down(list, list[0].Length - 1, nomerStroki + 1, symbol);
        }

    }
}

void Left(Span<char> mc, int index, char symbol)
{
    for (var i = 0; i < index; i++)
    {
        mc[i] = symbol;
    }
}

void Up(IReadOnlyList<char[]> data, int index, int nomerStroki, char symbol)
{
    if (nomerStroki == 0) return;
    for (int i = 0; i < nomerStroki; i++)
    {
        data[i][index] = symbol;
    }
}

void Right(Span<char> mc, int from, char symbol)
{
    int i;
    for (i = from; i < mc.Length; i++)
    {
        mc[i] = symbol;
    }

}

int Down(IReadOnlyList<char[]> data, int index, int nomerStroki, char symbol)
{
    if (data.Count == nomerStroki)
    {
        return nomerStroki;
    }
    for (var i = nomerStroki; i < data.Count; i++)
    {
        data[i][index] = symbol;
    }
    return index;
};