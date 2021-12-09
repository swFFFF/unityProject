print("***********多脚本执行***********")

--全局变量
a = 1
c = "123"

for i =  1,2 do
	c = "swf"
end

print(c)
--局部变量 关键字 local
for i =  1,2 do
	local d = "swf"
	print("循环中"..d)
end
print(d)

fun = function ()
	local x = "123123"
end
fun()
print(x)
local x2 = "321321"
print(x2)

--多脚本执行
--关键字 require("脚本名") require('脚本名')
require("Test")
print(testA)
print(testlocalA)
--脚本卸载
--如果是require加载执行的脚本 加载一次过后不会再被执行
require("Test")
--package.loaded["脚本名"]
--返回值boolean 表示该脚本是否被执行
print(package.loaded["Test"])
--卸载已经执行过的脚本
package.loaded["Test"] = nil
print(package.loaded["Test"])
--require 执行脚本时 可以在脚本最后返回值
local testAA = require("Test")
print(testAA)


--大G表
--_G表是一个总表(table) 他讲我们申明的所有全局变量都存储在其中
for k,v in pairs(_G) do
	print(k,v)
end
--本地变量 local 修饰的变量不会存在大_G表中