/*
 * bitscan.cpp
 *
 *  Created on: 31 mar 2021
 *  Author: Darius Hanc
 */
#include "bitscan.h"

unsigned int ReverseIndex32(const unsigned int x) { return bit_scan_reverse<unsigned int>(x); }

unsigned int ForwardIndex32(const unsigned int x) { return bit_scan_forward<unsigned int>(x); }

unsigned int LengthBefore32(const unsigned int x) { return count_leading_zeros_ux<unsigned int>(x); }

unsigned int LengthAfter32(const unsigned int x) { return count_trailing_zeros_ux<unsigned int>(x); }

unsigned int ReverseIndex64(const unsigned long x) { return bit_scan_reverse<unsigned long>(x); }

unsigned int ForwardIndex64(const unsigned long x) { return bit_scan_forward<unsigned long>(x); }

unsigned int LengthBefore64(const unsigned long x) { return count_leading_zeros_ux<unsigned long>(x); }

unsigned int LengthAfter64(const unsigned long x) { return count_trailing_zeros_ux<unsigned long>(x); }