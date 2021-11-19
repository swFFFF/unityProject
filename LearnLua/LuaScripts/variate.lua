print("---------------变量----------------")
--lua中的简单变量类型
--nil number string boolean
--lua中所有的变量声明 都不需要声明变量类型 会自动判断
--类似c#中的var
--lua中的变量可以随便赋值 自动识别类型

--通过type函数 可以得到变量类型 返回值 string

--lua中使用没有声明的变量默认值是nil
print(b)
--nil 类似c# 中的null
a = nil
print(a)
print(type(a))
print(type(type(a)))
a = 1
print(a)
print(type(a))
a = 1.2
print(a)
print(type(a))
--字符串声明 使用单引号或者双引号包裹
--lua中 没有char
a = "123"
print(a)
print(type(a))
a = '123'
print(a)
print(type(a))
a = true
print(a)
print(type(a))

--lua中的复杂数据类型
--函数 function
--表 table
--数据结构 userdata
--协同程序 thread(线程)