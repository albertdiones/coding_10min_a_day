{$mode objfpc}
program day81;

uses
  SysUtils, RegExpr;

var
  cardNumber: string;
  r: TRegExpr;

begin
  Writeln('Please input your card number');
  ReadLn(cardNumber);

  r := TRegExpr.Create;
  try
    r.Expression := '^\d{15,16}$';
    if r.Exec(cardNumber) then
    begin
      Writeln('your card is valid.');
    end
    else
    begin
      Writeln('Your card is invalid!!!!');
    end;
  finally
    r.Free;
  end;

end.