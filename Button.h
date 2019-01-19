#include<iostream>
#include<string>

using namespace std;

class Button abstract
{
protected:
	string name;
public:
	Button(const string &n = "Button");

	void SetName(const string &n);
	string GetName() const;

	virtual void Press() const abstract;
};

class WinButton : public Button
{
public:
	void Press() const override;
};

class MacButton : public Button
{
public:
	void Press() const override;
};

// ------------------------------
 
class CheckBox abstract
{
protected:
	string name;
	bool checked;
public:
	CheckBox(const string &n = "CheckBox", const bool &ch = false);

	void SetName(const string &n);
	string GetName() const;

	virtual void Press();
};

class WinCheckBox : public CheckBox
{
public:
	void Press() override;
};

class MacCheckBox : public CheckBox
{
public:
	void Press() override;
};

// ------------------------------

class WinFactory
{
public:
	WinButton createButton() const;
	WinCheckBox createCheckBox() const;
};

class MacFactory
{
public:
	MacButton createButton() const;
	MacCheckBox createCheckBox() const;
};