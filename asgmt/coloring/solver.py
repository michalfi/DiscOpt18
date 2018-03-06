#!/usr/bin/python
# -*- coding: utf-8 -*-

import os
from subprocess import Popen, PIPE


def solve_it(input_data):
    with open("data/assignment.txt", "a") as asgmt:
        asgmt.write(input_data)
        asgmt.write("\r\n\r\n")
    process = Popen(['../../ColoringSolver/ColoringSolver/bin/Release/ColoringSolver.exe', '-s=cp'], stdout=PIPE, stdin=PIPE)
    (stdout, stderr) = process.communicate(input=input_data.encode())
    return stdout.decode().strip()


import sys

if __name__ == '__main__':
    import sys
    if len(sys.argv) > 1:
        file_location = sys.argv[1].strip()
        with open(file_location, 'r') as input_data_file:
            input_data = input_data_file.read()
        print(solve_it(input_data))
    else:
        print('This test requires an input file.  Please select one from the data directory. (i.e. python solver.py ./data/gc_4_1)')

