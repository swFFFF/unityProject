print("------------运算符-------------")
--算术运算符
-- + - * / % ^
--没有自增自减 ++ --
--没有复合运算符 += -= /= *= %=

print("加法运算" .. 1 + 2)
a = 1
b = 2
print(a + b)
--字符串 可以进行 算数运算符操作 
print("123.4" + 2)

print("减法" .. 1 - 2)
print("123.4" - 1)
print("乘法" .. 1 * 2)
print("123.4" * 2)
print("除法" .. 1 / 2)
print("123.4" / 2)
print("余法" .. 1 % 2)
print("123.4" % 2)

--^ lua中是幂运算
print("幂运算" .. 1 ^ 2)
print("123.4" ^ 2)

--条件运算符
-- > < >= <= == ~=
print(3 > 1)
print(3 ~= 1)
--逻辑运算符
--&& || ! "短路"
--and or not
print(true and false)
print(true or false)
print(true not false)
print(false and print("123"))
--位运算符
-- & | 不支持位运算符
--三目运算符
-- ? : 不支持三目运算符