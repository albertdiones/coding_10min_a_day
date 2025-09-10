

puts "Please input your card number"


cardNumber = gets

cardNumber = cardNumber.strip!

if /^\d{16}$/.match(cardNumber)
    puts "Your card number is valid"
else
    puts "Your card number is invalid!!!!"
end