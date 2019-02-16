<?php
/**
 * Copyright 2019 Stefano Moioli <smxdev4@gmail.com>
 */
if($argc < 2){
	fprintf(STDERR, "Usage: %s [ms-*.test] [prefix]\n", $argv[0]);
	return 1;
}

$prevLine = NULL;

$prefix = ($argc > 2) ? $argv[2] : "Test";


$f = fopen($argv[1], "r") or die("failed to open input\n");
$testNumber = 1;
while(!feof($f)){
	$line = rtrim(fgets($f));
	if(empty($line))
		continue;

	if(!is_null($prevLine) && @$line[0] == ';'){
		$find = "; CHECK: ";
		if(strpos($line, $find) !== 0)
			continue;
		$expected = substr($line, strlen($find));
		$expected = addslashes($expected);
		$out = <<<EOT
[Test]
public void {$prefix}{$testNumber}(){
	AssertMangling("$prevLine", "$expected");
}
EOT;
		$testNumber++;
		print($out . PHP_EOL);
	} else {
		$prevLine = $line;
	}
}
fclose($f);
?>
