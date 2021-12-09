print("*********迭代器遍历*******")
--#获取长度不准确 一般不使用
a = {[0] = 1, 2, [-1] = 3, 4, 5, [5] = 6}

--ipairs 从1开始往后遍历 小于等于0的索引无法获得 前参数为key后参为value
--只能找到连续索引的 键 中间断序 也无法遍历后面的内容
for i,k in ipairs(a) do
	print("ipairs遍历键值"..i.."_"..k)
end

--能够通过键找到所有值
for i,v in pairs(a) do
	print("pairs遍历键值"..i.."_"..v)
end

for i in pairs(a) do
	print("pairs遍历键"..i)
end