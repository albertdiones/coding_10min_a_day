using System;

int x1 = 1;
Console.WriteLine("Press any key to increment; q to exit");
Console.WriteLine(x1);
int x2 = x1;

// 1,1,2,3,5,8
while (
    true
) {
  ConsoleKeyInfo input = Console.ReadKey(true);
  if (input.KeyChar == 'q')   {
    Console.WriteLine("Exiting...");
    break;
  }
  
  int sum = x1 + x2;
  x1 = x2; 
  x2 = sum;

  Console.WriteLine(sum.ToString());
}
