print("************循环语句************")
--while语句 while...do...end
num = 0
while num < 5 do
	num = num + 1
	print(num)
end

--repeat...until 条件 条件是结束条件
num = 0
repeat
	print(num)
	num = num + 1
until num > 5 --满足条件 结束循环

--for语句
for i = 0,5 do --默认递增 
	print("ha")
end

for i = 6,1,-1 do --自定义增量
	print(i)
end