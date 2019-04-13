#include <iostream>

using namespace std;

template<typename T>
class Vector
{
	T *buffer = nullptr;
	size_t size = 0;
	size_t capacity = 0;
public:
	void reserve(const size_t &newC);
	void resize(const size_t &newS, const T &def = T());
	void pushBack(const T &elem);
	void popBack();

	Vector(const size_t &s = 0, const T &def = T());
	Vector(const Vector &v);

	void setValue(const size_t &in, const T &val);
	T getValue(const size_t &in) const;

	T &operator [](size_t in);
	T operaator[](size_t in) const;
	Vector<T> operator = (Vector v);

	bool empty() const;

	T &front();
	T &back();

	void clear();

	size_t getCapacity() const;
	size_t getSize() const;

	void print();

	~Vector();
};

template<typename T>
inline void Vector<T>::reserve(const size_t &newC)
{
	if (newC <= capacity)
		return;
	T * buf = new T[newC];
	for (int i = 0; i < size; i++)
		buf[i] = buffer[i];
	delete []buffer;
	buffer = buf;
	capacity = newC;
}

template<typename T>
inline void Vector<T>::pushBack(const T & elem)
{
	resize(size + 1, elem);
}

template<typename T>
inline void Vector<T>::popBack()
{
	resize(size - 1);
}

template<typename T>
inline Vector<T>::Vector(const size_t & s, const T & def)
{
	resize(s, def);
}

template<typename T>
inline Vector<T>::Vector(const Vector & v)
{
	reserve(v.capecity);
	resize(v.size);
	for (int i = 0; i < size; i++)
		buffer[i] = v[i];
}

template<typename T>
inline void Vector<T>::setValue(const size_t & in, const T & val)
{
	if (in < size)
		buffer[in] = val;
}

template<typename T>
inline T Vector<T>::getValue(const size_t & in) const
{
	if (in < size)
		return buffer[in];
}

template<typename T>
inline T & Vector<T>::operator[](size_t in)
{
	static T bad;
	if (in < size)
		return buffer[in];
	else
		return bad;
}

template<typename T>
inline Vector<T> Vector<T>::operator=(Vector v)
{
	capecity = 0;
	size = 0;
	reserve(v.capacity);
	resize(v.size);
	for (int i = 0; i < size; i++)
		buffer[i] = v[i];
	return *this;
}

template<typename T>
inline bool Vector<T>::empty() const
{
	return (size > 0)? 1 : 0;
}

template<typename T>
inline T & Vector<T>::front()
{
	static T bad;
	if (size > 0)
		return buffer[0];
	return bad;
}

template<typename T>
inline T & Vector<T>::back()
{
	static T bad;
	if (size > 0)
		return buffer[size - 1];
	return bad;
}

template<typename T>
inline void Vector<T>::clear()
{
	resize(0);
}

template<typename T>
inline size_t Vector<T>::getCapacity() const
{
	return capacity;
}

template<typename T>
inline size_t Vector<T>::getSize() const
{
	return size;
}

template<typename T>
inline void Vector<T>::resize(const size_t &newS, const T &def)
{
	if (newS > capacity)
		reserve(newS*1.3);
	for (int i = size; i < newS; i++)
		buffer[i] = def;
	size = newS;
}

template<typename T>
inline void Vector<T>::print()
{
	for (int i = 0; i < size; i++)
		cout << buffer[i] << "\t";
	cout << endl;
	cout << "Size: " << size << " Capacity: " << capacity << endl;
 }

template<typename T>
inline Vector<T>::~Vector()
{
	delete[]buffer;
}

