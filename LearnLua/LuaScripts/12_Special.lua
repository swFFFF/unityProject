print("******************特殊用法********************")

print("******************多变量赋值********************")
local a,b,c = 1,2,"123"
print(a)
print(b)
print(c)

--多变量赋值 如果后面的值不够 会自动补空
a,b,c = 1,2
print(a)
print(b)
print(c)

--多变量赋值 如果后面的值多了 自动忽略
a,b,c = 1,2,3,4,5
print(a)
print(b)
print(c)



print("******************多返回值********************")
--多返回值同理
function Test( ... )
	return 1,2,3,4
end

a,b,c = Test()
print(a)
print(b)
print(c)

print("******************and or********************")
--逻辑与 逻辑或
--and or 不仅可以连接 boolean 任何东西都可以连接
--lua中 只有nil 和 false 才认为是假
--"短路" 对于and来说 有假才为假 对于or来说 有真则真
--只需要判断 第一个 第一个满足 就会停止计算
print(1 and 2)
print(0 and 1)
print(nil and 2)

print(true or 1)
print(false or 2)

--lua模拟三目运算符
x = 3
y = 2
local res = (x > y) and x or y
print(res)
