augroup omnisharp_commands
  autocmd!

  autocmd FileType cs nmap <silent> <buffer> gd <Plug>(omnisharp_go_to_definition)
  " ... other omnisharp-vim mappings
augroup END
