my $input = '91212129';
my $firstChar = substr($input, 0,1);
my $sum = 0;
my $lastChar;

my $len = length($input);

foreach (my $i = 0; $i < $len; $i++){
    my $char = substr($input, $i, 1);
    if ($char == $lastChar){
        $sum += int($char);
    }
    
    if ($i == ($len - 1) && $char == $firstChar){
        $sum += int($char);
    }
    $lastChar = $char;
}

print "Sum = $sum";
