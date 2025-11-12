#include <stdint.h>
int main()
{
    struct C
    {
        uint8_t visible;
        uint64_t x, y;
    };
    struct C p;
    p.x = 0;
    return 0;
}