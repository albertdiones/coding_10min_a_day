using System;

int x = 0;
Console.WriteLine("Press any key to increment; q to exit");
Console.WriteLine(x);
while (
    true
) {
  ConsoleKeyInfo input = Console.ReadKey(true);
  if (input.KeyChar == 'q')   {
    Console.WriteLine("Exiting...");
    break;
  }
  x++;
  Console.WriteLine(x.ToString());
}
