print("*****************自带库*****************")
--string
--table
print("*****************时间*****************")
--系统时间
print(os.time())
--自己传入参数 得到时间
print(os.time({year = 2022, month = 1, day = 27}))

--os.date("*t")
local nowTime = os.date("*t")
print(nowTime)
for k,v in pairs(nowTime) do
	print(k, v)
end
print(nowTime.hour)

print("*****************数学运算*****************")
--math
--绝对值
print(math.abs(-11))
--弧度转角度
print(math.deg(math.pi))
--向上下取整
print(math.floor(2.6))
print(math.ceil(5.2))
--最大最小值
print(math.max(1,2))

--小数分离 分成整数和小数部分
print(math.modf(1.2))

--幂运算
print(math.pow(2, 5))

--随机数
--先设置随机数种子
math.randomseed(os.time())
print(math.random(100))
print(math.random(100))

--开方
print(math.sqrt(4))
print("*****************路径*****************")
--lua脚本加载路径
print(package.path)
package.path = package.path..";D:\\"
print(package.path)