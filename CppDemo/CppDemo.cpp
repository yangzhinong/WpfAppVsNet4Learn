// CppDemo.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <type_traits>

void foo(char*);
void foo中文(int);

int main()
{
	if (std::is_same<decltype(NULL), decltype(0)>::value)
		std::cout << "NULL == 0" << std::endl;
	if (std::is_same<decltype(NULL), decltype((void*)0)>::value)
		std::cout << "NULL == (void *)0" << std::endl;
	if (std::is_same<decltype(NULL), std::nullptr_t>::value)
		std::cout << "NULL == nullptr" << std::endl;

	foo中文(0);

	foo(nullptr);

	std::cout << "Hello World!\n";
	std::cin.get();
	return 0;
}

void foo中文(int i) {
	std::cout << "foo(int) is called" << std::endl;
}

void foo(char*) {
	std::cout << "foo(char*) is called" << std::endl;
}