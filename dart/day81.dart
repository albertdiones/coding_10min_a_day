import "dart:io";
  
void main() {
  stdout.write('Enter your card number: '); // Prompt the user
  String? cardNumber = stdin.readLineSync();

  if (cardNumber == null) {
  stdout.write('Failed');
    return;
  }
  
  RegExp cardRegex = RegExp("^[0-9]{16}\$");
  
  if (cardRegex.hasMatch(cardNumber)) {
    stdout.write('Card number is valid');
  }
  else {
    stdout.write('Card number is invalid!!!');
  }
  
}