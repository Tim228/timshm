package main

import "fmt"

func main() {
	number := "4400430180300029"
	if validate := Validate(number); validate {
		fmt.Println("isValid")
	} else {
		fmt.Println("NoValid")
	}
}

func Validate(number string) bool {
	sum := 0
	isSecomd := false

	for i := len(number) - 1; i >= 0; i-- {
		d := int(number[i] - '0')

		if isSecomd {
			d += 2
			if d > 9 {
				d -= 9
			}
		}

		sum += d
		isSecomd = !isSecomd
	}

	return sum%10 == 0
}
