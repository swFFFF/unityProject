print("*************复杂数据类型--表*************")
a = {["姓名"] = "shiwewnfeng",["age"] = 18,[true] = 22}
--可以通过括号加键的形式访问
print(a["姓名"])
print(a["age"])
print(a[true])
--可以用.的形式访问 键不能是中文和数字
print(a.age)
--修改
a["age"] = 22
print(a["age"])
--新增
a["sex"] = true;
--删除
a["sex"] = nil

--遍历
for k,v in pairs(a) do
	print(k,v)
end

--也是先获得键再取值
for _,v in pairs(a) do
	print(v)
	--等同print(_,v)
end

--lua中默认没有面向对象 要自己实现
--成员变量 成员函数等 用,隔开
Student = 
{ 
	age = 18, 
	sex = true,
	Up = function()
		--这样写 这个age 和表中的age没有任何关系 它是一个全局变量
		--print(age)
		--想要在表内部函数中 调用表本身的属性或方法
		--一定要指定是谁的 所以要使用 表名.表名.方法
		print("test function")
		print(Student.age)
		print("test function")
	end,
	Learn = function(t)
		--第二种 能够在函数内部调用自己属性或者发方法的 方法
		--把自己作为一个参数传入 在内部访问
		print(t.sex)
		print("test function2")
	end,
}

Student.Learn(Student)
--lua中.和:区别 :调用方法 会默认把调用者 作为第一个参数传入方法
Student:Learn()

Student.name = "swf"
Student.Speak = function()
	print("说话1")
end
--函数的第三种声明方式 : 只能在表外部声明
function Student:Speak2()
	--lua中 有一个关键字 self 表示 默认传入的第一个参数 不是C#中的tis
	print(self.name .. "说话".. self.name)
end
--lua中像是静态变量和静态函数
Student.Up()
Student.Speak()
Student:Speak2()

--表的公共操作
t1 = {{age = 1, name = "123"}, {age = 2, name = "345"}}
t2 = {name = "swf", sex = true}

--插入
print(#t1)
table.insert(t1,t2)
print(#t1)
print(t1[1])
print(t1[2])
print(t1[3])
print(t1[3].sex)
print(t2)
--删除指定元素
--remove方法 传表进去 会移除最后一个索引的内容
table.remove(t1)
print(#t1)
print(t1[1].name)
print(t1[2].name)
print(t1[3])

--remove方法 传两个参数 第一个参数 是要移除内容的表
--第二个参数 是要移除内容的索引
table.remove(t1, 1)
print(t1[1].name)
print(#t1)

print("********排序********")
t2 = {5,2,7,9,5}
-- table.sort(t2)
-- 第二个参数是排序规则函数
table.sort( t2, function ( a,b )
	if a < b then
		return true
	end
end 
)
for _,v in pairs(t2) do
	print(_,v)
end

print("********拼接**********")
tb = {"123", "456", "789"}
--连接函数 用于拼接表中元素 返回值是一个字符串
str = table.concat( tb, "; " )
print(str)

