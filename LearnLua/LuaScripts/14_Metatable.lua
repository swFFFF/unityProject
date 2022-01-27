print("****************元表****************")

print("****************元表概念****************")
--任何表变量都可以作为另一个表变量的元表
--任何表变量都可以有自己的元表（爸爸）
--当子表（有元表的表）中进行一些特定操作时
--会执行元表中的内容
print("****************设置元表****************")
meta = {}
myTable = {}
--设置元表函数
--第一个参数 子表
--第二个参数 元表（爸爸）
setmetatable(myTable, meta)

print("****************特定操作****************")
print("****************特定操作— __tostring****************")
meta2 = {
	--当子表要被当作字符串使用时 会默认调用这个元表中的__tostring方法
	__tostring = function(t)
		print("meta2")
		return t.name
	end
}
myTable2 = {
	name = "swift",
	a = function()
		print("222")
	end
}
--设置元表函数
--第一个参数 子表
--第二个参数 元表（爸爸）
setmetatable(myTable2, meta2)
--print() 参数转换成string 调用tostring
print(myTable2)

print("****************特定操作— __call****************")
meta3 = {
	--当子表要被当作字符串使用时 会默认调用这个元表中的__tostring方法
	__tostring = function(t)
		print("tostring")
		return t.name
	end,
	--当子表被当做一个函数来使用时 会默认调用这个__call中的内容
	--当希望传参时 默认第一个参数是调用者自己
	__call = function(a,b)
		print(a)
		print(b)
		print("call")
	end
}
myTable3 = {
	name = "swift"
}
--设置元表函数
--第一个参数 子表
--第二个参数 元表（爸爸）
setmetatable(myTable3, meta3)
--把子表当函数使用 就会调用元表的 __call方法
myTable3(1)
-- print(myTable3);

print("****************特定操作— 运算符重载****************")
--详见文档
meta4 = {
	--相当于运算符重载 当子表使用+运算符时 会调用该方法
	--运算符+
	__add = function(t1, t2)
		return t1.age + t2.age
	end,
	--运算符-
	__sub = function(t1, t2)
		return t1.age - t2.age
	end,
	--运算符*
	__mul = function(t1, t2)
		return t1.age * t2.age
	end,
	--运算符/
	__div = function(t1, t2)
		return t1.age / t2.age
	end,
	--运算符%
	__mul = function(t1, t2)
		return t1.age % t2.age
	end,
	--运算符^
	__mul = function(t1, t2)
		return t1.age ^ t2.age
	end,
}
myTable4 = {
	age = 2
}
setmetatable(myTable4, meta4)
myTable5 = {
	age = 5
}
print(myTable4 + myTable5)
print(myTable4 - myTable5)

print("****************特定操作— __index和__newindex****************")
meta6Father = {
	age = 2
}
meta6Father.__index = meta6Father

meta6 = {
	-- age = 1,
	-- __index = {age = 2}
}
--__index的初始化建议放在表外
meta6.__index = meta6

myTable6 = {}
setmetatable(meta6, meta6Father)
setmetatable(myTable6, meta6)
--得到元表
print(getmetatable(myTable6))

--__index 当子表中 找不到某一个属性时
--会到元表中 __index只限定的表去找属性
print(myTable6.age)
--rawget 使用它时 会去找自己身上是否有这个变量
print(rawget(myTable6, "age"))

--newindex 当赋值时，如果赋值一个不存在的索引(赋值重定向，不使用__newindex新建key)
--那么会把这个值赋值到newindex所指的表中 不会修改自己
meta7 = {}
meta7.__newindex = {}
myTable7 = {}
setmetatable(myTable7, meta7)
myTable7.age = 18
print(myTable7.age)
print("**********")
print(meta7.__newindex.age)
--rawset 忽略newindex的设置 指挥改变自己的变量
rawset(myTable7, "age", 2)
print(myTable7.age)