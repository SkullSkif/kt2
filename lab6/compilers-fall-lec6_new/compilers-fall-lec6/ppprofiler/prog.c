#include <stdio.h>
#include <unistd.h>

int my_func(int a, int b) {
    return a + b;
}

void my_second_func(int q) {
    for (int i = 0; i < q; i++);
}

int main(int argc, char** argv) {
    getpid();
    printf("Hello, World\n");
    my_second_func(5);
    return my_func(1, 2);
}