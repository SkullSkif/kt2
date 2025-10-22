target triple = "x86_64-pc-linux-gnu"

define dso_local i32 @gcd(i32 %a, i32 %b){
  %local_a = alloca i32
  %local_b = alloca i32
  %t = alloca i32
  store i32 %a, i32* %local_a 
  store i32 %b, i32* %local_b
  br label %whilecond


whilecond:
  %cond_b = load i32, i32* %local_b
  %compare = icmp ne i32 %cond_b, 0
  br i1 %compare, label %loop, label %exit

loop:
  %loop_a = load i32, i32* %local_a
  %loop_b = load i32, i32* %local_b
  %local_t = srem i32 %loop_a, %loop_b
  store i32 %local_t, i32* %t  
  %new_b = load i32, i32* %local_b
  store i32 %new_b, i32* %local_a
  %new_t = load i32, i32* %t
  store i32 %new_t, i32* %local_b
  br label %whilecond


exit:
  %r_a = load i32, i32* %local_a
  ret i32 %r_a
}

@.str = private unnamed_addr constant [18 x i8] c"gcd(12, 15) = %d\0A\00", align 1

define dso_local i32 @main() {
  %1 = alloca i32
  store i32 0, i32* %1
  %2 = call i32 @gcd(i32 12, i32 15)
  %3 = call i32 (i8*, ...) @printf(i8* getelementptr ([18 x i8], [18 x i8]* @.str, i64 0, i64 0), i32 %2)
  ret i32 0
}

declare i32 @printf(i8*, ...)
