print("************面向对象**************")

print("************封装**************")
--面向对象 类 其实都是基于 table来实现
--元素相关的知识点
Object = {}
Object.id = 1

function Object:Test()
	print(self.id)
end
--冒号: 会自动调用这个函数对象 作为第一个参数传入的写法 (哪个对象调用方法里self就是谁)
function Object:new()
	--self 代表默认传入的第一个参数
	--对象就是变量 返回一个新的
	--返回出去的内容 本质上就是表对象
	local obj = {}
	--元表知识 __index 当找自己的变量 找不到时 就会去找元素表当中 __index指向的内容
	self.__index = self
	setmetatable(obj, self)
	return obj
end

local myObj = Object:new()
print(myObj)
print(myObj.id)
myObj:Test()

myObj.id = 2
myObj:Test()
print(Object.id)
print("************继承**************")
--写一个用于继承的方法
function Object:subClass(className)
	--_G知识点 是总表 所有声明的全局变量都以键值对的形式存在其中
	_G[className] = {}
	--写相关继承的规则
	--用到元表
	local obj = _G[className]
	self.__index = self
	--子类 定义一个base属性
	obj.base = self
	setmetatable(obj, self)
end
-- print(_G)
-- _G["a"] = 1
-- _G.b = "123"
-- print(a)
-- print(b)
Object:subClass("Person")
print(Person)
print(Person.id)

local p1 = Person:new()
print(p1.id)
p1.id = 100
print(p1.id)

Object:subClass("Monster")
local m1 = Monster:new()
print(m1.id)
m1.id = 200
print(m1.id)
print("************多态**************")
--相同行为 不同表现 就是多态
--相同方法 不同执行逻辑 就是多态
Object:subClass("GameObject")
GameObject.posX = 0
GameObject.posY = 0
function GameObject:Move()
	self.posX = self.posX + 1
	self.posY = self.posY + 1
	print(self.posX)
	print(self.posY)
end
GameObject:subClass("Player")
function Player:Move()
	--这里base是GameObject表
	--这种方式调用 相当于是把基类表作为第一个参数传入方法
	--避免把基类表传入到方法中 这样相当于是共用一张表（GameObject）的属性
	--如果要执行父类逻辑 不要直接使用冒号调用
	--要通过.调用 然后自己传入第一个参数
	--self.base:Move()
	self.base.Move(self)
end

local p1 = Player:new()
p1:Move()
p1:Move()
--这种写法有问题 不同对象使用的成员变量 是相同的
--不是自己的
local p2 = Player:new()
p2:Move()