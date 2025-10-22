target triple = "x86_64-unknown-linux-gnu"

@.str = private unnamed_addr constant [18 x i8] c"gcd(12, 15) = %d\0A\00", align 1

define dso_local i32 @gcd(i32 %0, i32 %1) {
  %3 = alloca i32
  %4 = alloca i32
  %5 = alloca i32
  store i32 %0, ptr %3
  store i32 %1, ptr %4
  br label %6

6:                                                ; preds = %9, %2
  %7 = load i32, ptr %4                           ; загрузка б, проверка на 0 и перемещени в конец или начало цикла
  %8 = icmp ne i32 %7, 0
  br i1 %8, label %9, label %15

9:                                                ; preds = %6
  %10 = load i32, ptr %3
  %11 = load i32, ptr %4
  %12 = srem i32 %10, %11                         ; t = a % b
  store i32 %12, ptr %5
  %13 = load i32, ptr %4
  store i32 %13, ptr %3
  %14 = load i32, ptr %5
  store i32 %14, ptr %4
  br label %6                                     ; типа бесконечноый цикл, но он конечный, хоть мы и не знаем то сколько раз он будет работать

15:                                               ; preds = %6
  %16 = load i32, ptr %3                 ; выход из функции и возврат значения
  ret i32 %16
}

; Function Attrs: noinline nounwind optnone uwtable
define dso_local i32 @main() {
  %1 = alloca i32
  store i32 0, ptr %1
  %2 = call i32 @gcd(i32 12, i32 15)
  %3 = call i32 (ptr, ...) @printf(ptr @.str, i32 %2)
  ret i32 0
}

declare i32 @printf(ptr, ...)
