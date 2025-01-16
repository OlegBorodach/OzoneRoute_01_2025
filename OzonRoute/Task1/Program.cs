using var input = new StreamReader(File.Open(Path.Combine(Directory.GetCurrentDirectory(), "3"), FileMode.Open));


var n = Convert.ToInt16(input.ReadLine());

for (short i = 0; i < n; i++)
{
    var zp = input.ReadLine().AsSpan();
    switch (zp.Length)
    {
        case 1:
            Console.WriteLine(0);
            continue;
        case 2:
            Console.WriteLine(zp[0] > zp[1] ? zp[0] : zp[1]);
            continue;
    }

    for (var j = 0; j < zp.Length; j++)
    {
        if (j<zp.Length-1&&zp[j] >= zp[j + 1])
        {
            continue;
        }

        var z1 = zp[..j];
        var z2=zp[(j+1)..];

        Console.WriteLine(string.Concat(z1,z2));
        break;
    }
}