print("***************函数**************")
-- a = function function_name( ... )
-- 	-- body
-- end

--无参无返回值
function F1()
	print("F1")
end
F1()

--类似C#中的委托和事件
F2 = function ()
	print("F2函数")
end
F2()

--有参数
function F3(a)
	print(a)
end
F3(1)
F3("2")
F3(true)
--如果传入参数和函数参数个数不匹配
--会进行补空nil或者丢弃
F3()
F3(1,2,3)

--有返回值
--多返回值时 前面申明多个变量来接收
--如果变量不够 不印象 直接取对应位置的返回值
--变量多了 赋值nil
function F4(a)
	return a,"123",true
end
temp,temp1,temp2,temp3 = F4("123")
print(temp)
print(temp1)
print(temp2)

--函数的类型 就是 function
F5 = function()
	print("123")
end
print(type(F5))

--函数的重载
--函数名相同 参数类型不同 或者参数个数不同 
--lua中函数不支持重载
--默认调用最后一个声明的函数
function F6()
	print("F6")
end
function F6(str)
	print(str)
end
F6()

--变长参数 ...
function F7( ... )
	--变长参数使用 用一个表存起来
	arg = {...}
	for i = 1, #arg do
		print(arg[i])
	end
end
F7(1,"123",true,4,5,6,7)

--函数嵌套
function F8()
	return function()
		print(123)
	end
	
end

f9 = F8()
f9()

--闭包
function F9(x)
	--改变传入参数的生命周期
	return function(y)
		return x + y
	end
end

f10 = F9(10)
print(f10(5))