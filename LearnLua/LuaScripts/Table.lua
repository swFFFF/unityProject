print("复杂数据类型 table")
--所有复杂类型都是靠table(类)

--数组
a = {1,2,4,"132",true,nil,99}
--lua中索引从1开始
print(a[5])
-- #是通用获取长度关键字
--计算长度时 nil被忽略且不会继续往后计算长度
print(#a)
--数组遍历
for i = 1 ,#a do
	print(a[i])
end

--二维数组
a = {{1,2,3},{4,5,6}}
print(a[2][3])

--二维数组便利
for i = 1, #a do
	b = a[i]
	for j = 1, #b do
		print(b[j])
	end
end

--自定义索引
print("-----------")
aa = {[0] = 1, 2,3,[-1] = 4, 5,[4] = 6, [7] = 7}
print(aa[0])
print(aa[-1])
print(aa[1])
print(aa[2])
print(aa[3])
print(aa[4])
print(aa[7])
--只计算顺延个数
print(#aa)

aaa = {[1] = 1, [2] = 2,[4] = 4, [6] = 6}
print(#aaa)
for i = 1, #aaa do
	print(aaa[i])
end