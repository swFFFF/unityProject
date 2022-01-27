print("*************协同程序*************")
--和线程区别 同一时间只能有一个协程在运行直到切换或者
print("*************协程创建*************")
--coroutine.create() 常用 返回thread
fun = function()
	print(123)
end

co = coroutine.create(fun)
--lua协程本质是一个线程对象
print(co)
print(type(co))

--coroutine.warp() 返回function
co2 = coroutine.wrap(fun)
print(co2)
print(type(co2))

print("*************协程运行*************")
--create 创建的协程
coroutine.resume(co)
--warp 
co2()
print("*************协程挂起*************")
fun2 = function()
	local i = 1
	while true do
		print(i)
		i = i + 1
		print(coroutine.status(co3))
		--可以获得当前正在运行的线程号
		print(coroutine.running())
		--协程的挂起函数
		coroutine.yield(6)
	end
end

--第一个返回值 协程是否启动成功
--第二个 yield的返回值
co3 = coroutine.create(fun2)
isOk, temp1 = coroutine.resume(co3)
print(isOk, temp1)
isOk, temp1 = coroutine.resume(co3)
print(isOk, temp1)
isOk, temp1 = coroutine.resume(co3)
print(isOk, temp1)


--wrap没有第一个返回值
co4 = coroutine.wrap(fun2)
print("warp ".. co4())
print("warp ".. co4())
print("warp ".. co4())
print("*************协程状态*************")
--coroutine.status(协程对象)
--dead 结束
--suspended 中止
--running 运行
print(coroutine.status(co3))
print(type(coroutine.status(co3)))
print(coroutine.status(co))

