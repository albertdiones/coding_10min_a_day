/**
  212  wget https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
  213  dpkg -i packages-microsoft-prod.deb
  214  sudo dpkg -i packages-microsoft-prod.deb
  216  sudo apt install -y apt-transport-https
  217  sudo apt install -y dotnet-sdk-8.0
  221  dotnet tool install -g dotnet-script
*/

int charAsInt = 17;

// 1.7 -> 1
// 1.0
double x = Math.Floor((decimal)(charAsInt / 10));

System.Console.WriteLine(x.ToString("R"));
