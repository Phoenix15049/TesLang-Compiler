using TesLang_Compiler;

string code = @"
            fn arithmetic_operations(a as int, b as int) <int> {
    sum :: int = a + b;
    difference :: int = a - b;
    product :: int = a * b;
    quotient :: int = a / b;

    result :: int = sum + difference + product + quotient;
   
    while [[result < 100]]
    begin
        result = result + 10;
    end

    do
    begin
        result = result - 5;
    end
    while [[result > 50]]

    return result;
}

fn vector_operations(vec as vector) <int> {
    result :: int = 0;
    for (i = 0 to length(vec) - 1)
    begin
        if [[vec[i] > 5]]
        begin
            result = result + vec[i];
        end
        else
        begin
            result = result + 1;
        end
    end

    return result;
}

fn nested_and_short_functions(a as int) <int> {
    fn nested_helper(x as int) <int> return x * 2;
   
    result :: int = nested_helper(a);

    fn another_short_function(y as int) <int> => return y + 3;

    result = result + another_short_function(result);

    return result;
}

fn function_without_body_example(x as int) <int> => return x + 42;

fn handle_null_and_boolean(flag as bool, value as int) <null> {
    if [[flag]]
    begin
        print(""Flag is true"");
    end
    else
    begin
        print(""Flag is false"");
    end

    if [[value == null]]
    begin
        print(""Value is null"");
    end
    else
    begin
        print(""Value is not null"");
    end

    return;
}

fn complex_conditions_and_loops(a as int, b as int, vec as vector) <vector> {
    result :: vector = [];

    while [[a < b]]
    begin
        if [[a > 0]]
        begin
            append(result, a);
        end
        a = a + 1;
    end

    do
    begin
        append(result, b);
        b = b - 1;
    end
    while [[b > 0]]

    return result;
}

fn main () <null> {
    num :: int = 5;
    vec :: vector = [1, 2, 3, 4, 5];
    str :: str = ""Current value: "";
    status :: bool = true;

    <%% Initialize and test various functions %%>

    res1 :: int = arithmetic_operations(num, 10);
    res2 :: int = vector_operations(vec);
    res3 :: int = nested_and_short_functions(num);

    print(str + to_str(res1));
    print(str + to_str(res2));
    print(str + to_str(res3));

    function_without_body_example(res1);

    handle_null_and_boolean(status, num);

    fn inline_function(z as int) <int> return z - 1;

    res4 :: int = inline_function(res3);
    print(""Final result: "" + to_str(res4));

    <%% End of main function %%>

    return;
}

        ";

Lexer lexer = new Lexer(code);
List<Token> tokens = lexer.Tokenize();

TeslangParser parser = new TeslangParser(tokens);
try
{
    parser.Parse();
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}



























// string code = @"
//             fn sum(numlist as vector) <int> {
//                 result :: int = 0;
//                 for (i = 0 to length(numlist))
//                 begin
//                     result = result + numlist[i];
//                 end
//                 return result;
//             }
//         ";


// Lexer lexer = new Lexer(code);
// List<Token> tokens = lexer.Tokenize();




//Tokenizer output
// foreach (var token in tokens)
// {
//     Console.WriteLine(token);
// }