
//stl
import 'package:flutter/material.dart';

class MyHome extends StatelessWidget {
  const MyHome({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(

      body: Container(
        color: Colors.grey,
        child: Text('Welcome to my app',style: TextStyle(fontSize: 46.2
        ),
        ),
      ),

      drawer: Drawer(),
      appBar: AppBar(
        title: Text('Seha App'),
        backgroundColor: Colors.teal,
      ),

      floatingActionButton:Container(
        color: Colors.deepPurple,
        width: 370,
        height: 50,
      ) ,
      bottomNavigationBar:Container(
        color: Colors.lime,

        height: 80,
      )  ,



    );
  }
}
