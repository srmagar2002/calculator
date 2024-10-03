# **Basic WPF Calculator App**
This is a basic calculator application I created using Windows Presentation Foundation (WPF) in C#. It supports standard arithmetic operations such as addition, subtraction, multiplication, and division.

### **History Feature (2024-10-03)**
he History ListView component displays a log of previous calculations, allowing users to review or select past operations for reuse in the calculator.

**Features:**
- Displays the operands, operator, and result of each calculation in a stacked format.
- Supports auto-scrolling, showing the most recent calculations at the top.
- Limits the history to 20 entries to avoid clutter.
- Allows users to reselect a previous calculation from the history, reloading its operands and operator back into the calculator for further use.
- Press `CH` to clear history

### **Installation**

**Step 1: Clone the repo**
- Open a terminal (Command Prompt, PowerShell, or Git Bash).
- Run the following command to clone the repo.
  ```bash
  git clone https://github.com/srmagar2002/calculator.git

**Step 2: Open the project in Visual Studio**
- Open **Visual Studio**.
- Click **Open a project or solution**.
- Navigate to the clone repository folder, select the .sln (solution) file, and open it.

**Step 3: Run the application**
- Click Build > Build Soution or press `ctrl+shift+B` to complie the project
- Once the build is successful, press `f5` to run the application or click **Debug > Start Debugging**.

### **Some Screenshots**
![CALCGIF](/images/calc_showcase.gif)

![CALCPIC](/images/calc_showcase2.png)

**History Feature**
![CALCHISTORYGIF](/images/historyShowcase.gif)

![CALCHISTORYPIC](/images/history_showcase2.png)